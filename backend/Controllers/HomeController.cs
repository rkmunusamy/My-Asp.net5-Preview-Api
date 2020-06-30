namespace backend.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc; 

    [ApiController]
    [Route("[Controller]")]
    public class HomeController : ApiController
    {
         [Authorize]
        public ActionResult get()
        {
            return Ok("Works");
        }
        
    }
}
