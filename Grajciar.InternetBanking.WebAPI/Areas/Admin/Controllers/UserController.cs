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

        [HttpGet("{id}")]
        public IActionResult Get(int id) { 
            User user = _userAppService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            else { 
                return Ok(user);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user) {
            _userAppService.Create(user);
            return Ok("User has been created");
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] User user) {
            bool updated = _userAppService.Update(id, user);

            if (updated)
            {
                return Ok("User updated successfully");
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            bool deleted = _userAppService.Delete(id);

            if (deleted)
            {
                return Ok("User deleted successfully");
            }
            else
                return NotFound();
        }
    }
}
