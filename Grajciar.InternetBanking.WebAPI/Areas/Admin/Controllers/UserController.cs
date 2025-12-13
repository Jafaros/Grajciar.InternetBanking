using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class UserController : AdminBaseController
    {
        IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<UserDTO> users = _userAppService.Select();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            UserDTO? user = _userAppService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            else { 
                return Ok(user);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] UserUpdateDTO user) {
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
