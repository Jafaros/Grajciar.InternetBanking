using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Bank;
using Grajciar.InternetBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class BankController : AdminBaseController
    {
        IBankAppService _bankAppService;

        public BankController(IBankAppService bankAppService)
        {
            _bankAppService = bankAppService;
        }

        [HttpGet]
        public IActionResult Index() {
            var banks = _bankAppService.Select();
            return Ok(banks);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) { 
            var bank = _bankAppService.Get(id);

            if (bank == null)
            {
                return NotFound();
            }
            else { 
                return Ok(bank);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] BankCreateDTO bank) { 
            _bankAppService.Create(bank);
            return Ok(bank);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] BankUpdateDTO bank) { 
            bool success = _bankAppService.Update(id, bank);

            if (success)
            {
                return Ok(bank);
            }
            else {
                return NotFound();
            }
        }
    }
}
