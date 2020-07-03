namespace backend.Controllers
{
    using backend.Data.Models;
    using backend.Features;
    using backend.Features.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        public readonly UserManager<User> userManger;
        private readonly IIdentityService identityService;
        public readonly ApplicationSettings appSettings;

        public IdentityController(
            UserManager<User> userManger,
            IIdentityService identityService,
            IOptions<ApplicationSettings> appSettings)
        {
            this.userManger = userManger;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Username

            };
            var result = await this.userManger.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManger.FindByNameAsync(model.Username);
            if (user == null)
            {
                return this.Unauthorized();
            }
            var passwordValid = await this.userManger.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return this.Unauthorized();
            }
            // generate token that is valid for 7 days

            var Token = identityService.GenerateJwtToken(
                 user.Id,
                 user.userName,
                 this.appSettings.secret);

            return new LoginResponseModel
            {
                Token = Token
            };

        }
    }
}
