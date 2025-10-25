using Grajciar.InternetBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Admin home controller!");
        }
    }
}
