using Grajciar.InternetBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class HomeController : AdminBaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Admin home controller!");
        }
    }
}
