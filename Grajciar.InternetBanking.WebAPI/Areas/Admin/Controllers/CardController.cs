using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        ICardAppService _cardAppService;

        public CardController(ICardAppService cardAppService)
        {
            _cardAppService = cardAppService;
        }

        [HttpGet("{accountId}")]
        public IActionResult AccountCards(int accountId) {
            IList<Card> cards = _cardAppService.GetByAccount(accountId);
            return Ok(cards);
        }

        [HttpPost("{accountId}")]
        public IActionResult Create(int accountId, Card card) {
            _cardAppService.CreateForAccount(accountId, card);
            return Ok();
        }

        [HttpGet("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
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

        [HttpPut("{id}")]
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
