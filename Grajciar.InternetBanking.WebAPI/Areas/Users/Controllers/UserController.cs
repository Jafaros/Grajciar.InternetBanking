using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Users.Controllers
{
    [Route("[area]/[controller]")]
    [ApiController]
    public class UserController : UserBaseController
    {
        private readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "Self")]
        public IActionResult Update(int id, [FromBody] UserUpdateDTO user)
        {
            bool updated = _userAppService.Update(id, user);

            if (updated)
            {
                return Ok("User updated successfully");
            }
            else
                return NotFound();
        }
    }
}
