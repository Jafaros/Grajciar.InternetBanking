using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var user = _accountAppService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            else {
                return Ok(user);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult SelectByUser(int userId) { 
            return Ok(_accountAppService.SelectByUser(userId));
        }

        [HttpPost("{userId}")]
        public IActionResult Create(int userId, [FromBody] Account account) {
            if (_accountAppService.CreateForUser(userId, account))
            {
                return Ok();
            }
            else {
                return NotFound();
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] Account account) {
            if (_accountAppService.Update(id, account))
            {
                return Ok();
            }
            else {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (_accountAppService.Delete(id)) {
                return Ok();
            } else {
                return NotFound();
            }
        }
    }
}
