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
        public async Task<IActionResult> Index()
        {
            IList<UserDTO> users = await _userAppService.Select();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            UserDTO? user = await _userAppService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            else { 
                return Ok(user);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO user) {
            UserUpdateResponseDTO updated = await _userAppService.Update(id, user);

            if (updated.Success)
            {
                return Ok(updated);
            }
            else
                return NotFound(updated);
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
