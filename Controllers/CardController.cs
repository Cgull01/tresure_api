using System.Security.Claims;
using System.Text.Json.Serialization;
using API_tresure.Models;
using API_tresure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Enum;
using tresure_api.Data.Interfaces;
using tresure_api.Repository;

namespace tresure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IColumnRepository _columnRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserAccessService _userAccessService;

        public CardController(ICardRepository cardRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserAccessService userAccessService, IColumnRepository columnRepository)
        {
            _cardRepository = cardRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userAccessService = userAccessService;
            _columnRepository = columnRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardDTO>> GetCard(int id)
        {
            var card = await _cardRepository.GetCardById(id);

            if (card == null)
            {
                return NotFound();
            }

            if (!_userAccessService.IsOwner(card.Column.Project))
            {
                return NotFound();
            }

            var cardDTO = _mapper.Map<GetCardDTO>(card);

            return cardDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCardDTO>>> GetCards()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cards = await _cardRepository.GetCards();

            var userCards = cards.Where(c => c.Column.Project.Members.Any(m => m.UserId == userId));

            var cardsDTO = _mapper.Map<List<GetCardDTO>>(userCards);

            return cardsDTO;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCard(PostCardDTO card)
        {
            var column = await _columnRepository.GetColumnById(card.ColumnId);

            // If the column doesn't exist, return a 404 Not Found
            if (column == null)
            {
                return NotFound();
            }

            // Check if the user is authorized
            if (!_userAccessService.IsOwner(column.Project))
            {
                return Unauthorized();
            }

            // If the user is authorized, map the DTO to a Card and create it
            var newCard = _mapper.Map<Card>(card);
            newCard.Column = column;

            _cardRepository.CreateCard(newCard);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCard(EditCardDTO card)
        {
            Card updatedCard = await _cardRepository.GetCardById(card.Id);

            if (updatedCard == null)
            {
                return NotFound();
            }

            if (!_userAccessService.IsOwner(updatedCard.Column.Project))
            {
                return NotFound();
            }

            updatedCard = _mapper.Map<Card>(card);

            _cardRepository.UpdateCard(updatedCard);

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _cardRepository.GetCardById(id);

            if (card == null)
                return NotFound();

            if (!_userAccessService.IsOwner(card.Column.Project))
            {
                return NotFound();
            }

            _cardRepository.DeleteCard(card);
            return NoContent();
        }

    }
}
