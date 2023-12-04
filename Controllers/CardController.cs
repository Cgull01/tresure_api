using API_tresure.Models;
using API_tresure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IColumnRepository _columnRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly UserAccessService _userAccessService;

        public CardController(ICardRepository cardRepository, IMapper mapper, UserAccessService userAccessService, IColumnRepository columnRepository, IMemberRepository memberRepository)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _userAccessService = userAccessService;
            _columnRepository = columnRepository;
            _memberRepository = memberRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardDTO>> GetCard(int id)
        {
            Card card = await _cardRepository.GetCardById(id);

            if (card == null)
            {
                return NotFound(ErrorMessages.Messages[404]);
            }

            if (!_userAccessService.IsMember(card.Column.Project))
            {
                return Unauthorized(ErrorMessages.Messages[403]);
            }

            GetCardDTO cardDTO = _mapper.Map<GetCardDTO>(card);

            return cardDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCardDTO>>> GetCards()
        {
            var userId = _userAccessService.GetUserId();
            var cards = await _cardRepository.GetCards();

            var userCards = cards.Where(c => c.Column.Project.Members.Any(m => m.UserId == userId));

            List<GetCardDTO> cardsDTO = _mapper.Map<List<GetCardDTO>>(userCards);

            return cardsDTO;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCard(PostCardDTO card)
        {
            Column column = await _columnRepository.GetColumnById(card.ColumnId);

            // If the column doesn't exist, return a 404 Not Found
            if (column == null)
            {
                return BadRequest(ErrorMessages.Messages[400]);
            }

            // Check if the user is authorized
            if (!_userAccessService.IsTaskMaster(column.Project))
            {
                return Unauthorized(ErrorMessages.Messages[403]);
            }

            List<Member> assignedMembers = new List<Member>();

            // check if all assigned members exist AND belong to the project
            if(card.AssignedMembers != null)
            {
                foreach (AssignedMemberDTO member in card.AssignedMembers)
                {
                    Member dbMember = await _memberRepository.GetMemberById(member.Id);
                    if (dbMember == null || !column.Project.Members.Any(m => m.Id == member.Id))
                    {
                        return BadRequest(ErrorMessages.Messages[400]);
                    }
                    assignedMembers.Add(dbMember);
                }
            }

            // If the user is authorized, map the DTO to a Card and create it
            Card newCard = _mapper.Map<Card>(card);

            // directly add the fetched members
            newCard.AssignedMembers = assignedMembers;

            DateTime current_date = DateTime.Now.Date;
            newCard.CreationDate = current_date;

            _cardRepository.CreateCard(newCard);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCard(EditCardDTO card)
        {
            Card updatedCard = await _cardRepository.GetCardById(card.Id);

            if (updatedCard == null)
            {
                return NotFound(ErrorMessages.Messages[404]);
            }

            if (!_userAccessService.IsMember(updatedCard.Column.Project))
            {
                return Unauthorized(ErrorMessages.Messages[403]);
            }

            _mapper.Map(card, updatedCard);

            // Clear the existing assigned members
            updatedCard.AssignedMembers.Clear();

            // Add the new assigned members
            foreach (AssignedMemberDTO member in card.AssignedMembers)
            {
                var dbMember = await _memberRepository.GetMemberById(member.Id);
                if (dbMember == null || !updatedCard.Column.Project.Members.Any(m => m.Id == member.Id))
                {
                    return BadRequest(ErrorMessages.Messages[400]);
                }
                updatedCard.AssignedMembers.Add(dbMember);
            }

            _cardRepository.UpdateCard(updatedCard);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            Card card = await _cardRepository.GetCardById(id);

            if (card == null)
                return NotFound(ErrorMessages.Messages[404]);

            if (!_userAccessService.IsTaskMaster(card.Column.Project))
            {
                return Unauthorized(ErrorMessages.Messages[403]);
            }

            _cardRepository.DeleteCard(card);
            return NoContent();
        }

    }
}
