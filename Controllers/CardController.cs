using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Serialization;
using API_tresure.Models;
using API_tresure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
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
                return NotFound();
            }

            if (!_userAccessService.IsMember(card.Column.Project))
            {
                return NotFound();
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
                return NotFound();
            }

            // Check if the user is authorized
            if (!_userAccessService.IsTaskMaster(column.Project))
            {
                return Unauthorized();
            }

            List<Member> assignedMembers = new List<Member>();

            // check if all assigned members exist AND belong to the project
            if(card.AssignedMembers != null)
            {
                foreach (PostMemberDTO member in card.AssignedMembers)
                {
                    Member dbMember = await _memberRepository.GetMemberByUserId(member.UserId);
                    if (dbMember == null || !column.Project.Members.Any(m => m.UserId == member.UserId))
                    {
                        return UnprocessableEntity("One or more specified members do not exist.");
                    }
                    assignedMembers.Add(dbMember);
                }
            }

            // If the user is authorized, map the DTO to a Card and create it
            Card newCard = _mapper.Map<Card>(card);
            newCard.AssignedMembers = assignedMembers; // directly add the fetched members

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
                return NotFound();
            }

            if (!_userAccessService.IsTaskMaster(updatedCard.Column.Project))
            {
                return NotFound();
            }


            _mapper.Map(card, updatedCard);

            // Clear the existing assigned members
            updatedCard.AssignedMembers.Clear();

            // Add the new assigned members
            foreach (EditMemberDTO member in card.AssignedMembers)
            {
                var dbMember = await _memberRepository.GetMemberById(member.Id);
                if (dbMember == null || !updatedCard.Column.Project.Members.Any(m => m.Id == member.Id))
                {
                    return UnprocessableEntity("One or more specified members do not exist.");
                }
                updatedCard.AssignedMembers.Add(dbMember);
            }

            _cardRepository.UpdateCard(updatedCard);

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            Card card = await _cardRepository.GetCardById(id);

            if (card == null)
                return NotFound();

            if (!_userAccessService.IsTaskMaster(card.Column.Project))
            {
                return NotFound();
            }

            _cardRepository.DeleteCard(card);
            return NoContent();
        }

    }
}
