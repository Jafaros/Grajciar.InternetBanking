using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;
        private readonly IAccountAppService _accountAppService;
        public UserController(IUserAppService userAppService, IAccountAppService accountAppService)
        {
            _userAppService = userAppService;
            _accountAppService = accountAppService;

        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "Self")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO user)
        {
            UserUpdateResponseDTO updated = await _userAppService.Update(id, user);

            if (updated.Success)
            {
                return Ok(updated);
            }
            else
                return NotFound(updated);
        }

        [HttpGet("{id}/accounts")]
        [Authorize(Policy = "Self")]
        public IActionResult GetAccounts(int id)
        {
            var accounts = _accountAppService.SelectByUser(id);
            return Ok(accounts);
        }
    }
}
