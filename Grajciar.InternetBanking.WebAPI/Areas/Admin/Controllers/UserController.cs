using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<User> users = _userAppService.Select();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user) {
            _userAppService.Create(user);
            return Ok("User has been created");
        }
    }
}
