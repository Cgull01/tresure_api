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
    public class ColumnController : ControllerBase
    {
        private readonly IColumnRepository _columnRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserAccessService _userAccessService;
        private readonly IProjectRepository _projectRepository;

        public ColumnController(IColumnRepository columnRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserAccessService userAccessService, IProjectRepository projectRepository)
        {
            _columnRepository = columnRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userAccessService = userAccessService;
            _projectRepository = projectRepository;
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<GetCardDTO>> GetCard(int id)
        // {
        //     var card = await _columnRepository.GetCardById(id);

        //     if (card == null)
        //     {
        //         return NotFound();
        //     }

        //     if (!_userAccessService.isMember(card.Column.Project))
        //     {
        //         return NotFound();
        //     }

        //     var cardDTO = _mapper.Map<GetCardDTO>(card);

        //     return cardDTO;
        // }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<GetCardDTO>>> GetCards()
        // {
        //     var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //     var cards = await _columnRepository.GetCards();

        //     var userCards = cards.Where(c => c.Column.Project.Members.Any(m => m.UserId == userId));

        //     var cardsDTO = _mapper.Map<List<GetCardDTO>>(userCards);

        //     return cardsDTO;
        // }

        [HttpPost]
        public async Task<ActionResult> CreateColumn(PostColumnDTO column)
        {

            Project project = await _projectRepository.GetProjectById(column.ProjectId);

            if(project == null)
            {
                return NotFound();
            }

            if (!_userAccessService.IsOwner(project))
            {
                return Unauthorized();
            }

            Column newColumn = _mapper.Map<Column>(column);

            _columnRepository.CreateColumn(newColumn);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditColumn(EditColumnDTO column)
        {
            Column updatedColumn = await _columnRepository.GetColumnById(column.Id);

            if (updatedColumn == null)
            {
                return NotFound();
            }

            if (!_userAccessService.IsOwner(updatedColumn.Project))
            {
                return NotFound();
            }

            _mapper.Map(column, updatedColumn);

            _columnRepository.UpdateColumn(updatedColumn);

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn(int id)
        {
            Column column = await _columnRepository.GetColumnById(id);

            if (column == null)
                return NotFound();

            if (!_userAccessService.isTaskMaster(column.Project))
            {
                return NotFound();
            }

            _columnRepository.DeleteColumn(column);
            return NoContent();
        }

    }
}
