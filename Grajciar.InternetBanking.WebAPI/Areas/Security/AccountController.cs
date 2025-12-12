using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Security;
using Grajciar.InternetBanking.Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Security
{
    [Area("Security")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        ISecurityService _securityService;

        public AccountController(ISecurityService security)
        {
            _securityService = security;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var response = await _securityService.Register(dto, Roles.Customer);
            if (!response.Success) {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var response = await _securityService.Login(dto);

            if (!response.Success) {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}
