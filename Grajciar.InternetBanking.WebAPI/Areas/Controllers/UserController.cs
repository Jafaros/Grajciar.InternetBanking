using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Transaction;
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
        private readonly ICardAppService _cardAppService;
        private readonly ITransactionAppService _transactionAppService;
        private readonly IBankAppService _bankAppService;
        public UserController(IUserAppService userAppService, IAccountAppService accountAppService, ICardAppService cardAppService, ITransactionAppService transactionAppService, IBankAppService bankAppService)
        {
            _userAppService = userAppService;
            _accountAppService = accountAppService;
            _cardAppService = cardAppService;
            _transactionAppService = transactionAppService;
            _bankAppService = bankAppService;
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "Self")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] UserUpdateDTO user)
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

        [HttpGet("{id}/accounts/{accountId}")]
        [Authorize(Policy = "Self")]
        public IActionResult GetAccount(int id, int accountId)
        {
            var account = _accountAppService.Get(accountId);
            return Ok(account);
        }

        [HttpGet("{id}/accounts/{accountId}/cards")]
        [Authorize(Policy = "Self")]
        public IActionResult GetCardsForAccount(int id, int accountId)
        {
            var cards = _cardAppService.GetByAccount(accountId);
            return Ok(cards);
        }

        [HttpGet("{id}/accounts/{accountId}/transactions")]
        [Authorize(Policy = "Self")]
        public IActionResult GetTransactionsForAccount(int id, int accountId)
        {
            var transactions = _transactionAppService.GetByAccount(accountId);
            return Ok(transactions);
        }

        [HttpPost("{id}/transaction")]
        [Authorize(Policy = "Self")]
        public IActionResult CreateTransaction(int id, [FromBody] TransactionCreateDTO dto)
        {
            List<string> errors = _transactionAppService.Create(dto);

            if (errors.Count <= 0) return Ok();
            else return BadRequest(errors);
        }

        [HttpGet("banks")]
        public IActionResult GetBanks()
        {
            var banks = _bankAppService.Select();
            return Ok(banks);
        }
    }
}
