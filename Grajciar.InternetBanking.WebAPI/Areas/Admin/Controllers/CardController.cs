using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO;
using Grajciar.InternetBanking.Application.DTO.Card;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class CardController : AdminBaseController
    {
        ICardAppService _cardAppService;

        public CardController(ICardAppService cardAppService)
        {
            _cardAppService = cardAppService;
        }

        [HttpGet("Accounts/{accountId}/Cards")]
        public IActionResult AccountCards(int accountId) {
            IList<CardDTO> cards = _cardAppService.GetByAccount(accountId);
            return Ok(cards);
        }

        [HttpPost("Accounts/{accountId}/Cards")]
        public async Task<IActionResult> Create(int accountId, CardCreateDTO card) {
            await _cardAppService.CreateForAccount(accountId, card);
            return Ok();
        }

        [HttpGet("Cards/{id}")]
        public IActionResult Get(int id) {
            var card = _cardAppService.Get(id);

            if (card == null)
            {
                return NotFound();
            }
            else { 
                return Ok(card);
            }
        }

        [HttpDelete("Cards/{id}")]
        public IActionResult Delete(int id) { 
            bool succes = _cardAppService.Delete(id);

            if (succes)
            {
                return Ok("Card successfully deleted");
            }
            else { 
                return NotFound();
            }
        }

        [HttpPut("Cards/{id}/Block")]
        public IActionResult Block(int id) { 
            bool success = _cardAppService.Block(id);

            if (success)
            {
                return Ok();
            }
            else {
                return NotFound();
            }
        }

        [HttpPut("Cards/{id}/Unblock")]
        public IActionResult Unblock(int id)
        {
            bool success = _cardAppService.Unblock(id);

            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
