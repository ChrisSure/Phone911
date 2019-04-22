using Microsoft.AspNetCore.Mvc;

namespace Phone.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return Ok("Success!");
        }

    }
}
