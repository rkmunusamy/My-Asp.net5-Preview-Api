namespace backend.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
using backend.Data.Models;
using backend.Models.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityController : ApiController
    {
        public readonly UserManager<User> userManger;
        public readonly  ApplicationSettings  appSettings;
        public IdentityController(UserManager<User> userManger,IOptions<ApplicationSettings> appSettings)
        {
            this.userManger = userManger;
            this.appSettings = appSettings.Value;
        }

        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username

            };
                 var result = await this.userManger.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [Route(nameof(Login))]
        public async Task<ActionResult<string>> Login(LoginRequestModel model)
        {
            var user = await this.userManger.FindByNameAsync(model.Username);
            if(user == null)
            {
                return this.Unauthorized();
            }
            var passwordValid = await this.userManger.CheckPasswordAsync(user, model.Password);
            if(!passwordValid)
            {
                return this.Unauthorized();
            }
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);
            return encryptedToken;
        }
    }
}
