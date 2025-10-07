using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello world!");
        }
    }
}
