using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Account;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class AccountController : AdminBaseController
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var account = _accountAppService.Get(id);

            if (account == null)
            {
                return NotFound();
            }
            else {
                return Ok(account);
            }
        }

        [HttpGet("Users/{userId}")]
        public IActionResult SelectByUser(int userId) { 
            return Ok(_accountAppService.SelectByUser(userId));
        }

        [HttpPost("Users/{userId}/Account")]
        public IActionResult Create(int userId, [FromBody] AccountCreateDTO account) {
            if (_accountAppService.CreateForUser(userId, account))
            {
                return Ok();
            }
            else {
                return NotFound();
            }
        }

        [HttpPatch("User/Accounts/{id}")]
        public IActionResult Update(int id, [FromBody] AccountUpdateDTO account) {
            if (_accountAppService.Update(id, account))
            {
                return Ok();
            }
            else {
                return NotFound();
            }
        }

        [HttpDelete("Accounts/{id}")]
        public IActionResult Delete(int id) {
            if (_accountAppService.Delete(id)) {
                return Ok();
            } else {
                return NotFound();
            }
        }
    }
}
