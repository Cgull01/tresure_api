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
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly UserAccessService _userAccessService;

        public MemberController(IMapper mapper, UserAccessService userAccessService, IMemberRepository memberRepository, IProjectRepository projectRepository, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _userAccessService = userAccessService;
            _memberRepository = memberRepository;
            _projectRepository = projectRepository;
            _roleRepository = roleRepository;
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<GetCardDTO>> GetMember(int id)
        // {
        //     var card = await _cardRepository.GetCardById(id);

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetMemberDTO>>> GetMembers(int projectId)
        {

            string user_id = _userAccessService.GetUserId();

            var project = await _projectRepository.GetProjectById(projectId);

            if (!project.Members.Any(m => m.UserId == user_id))
            {
                return Forbid(ErrorMessages.Messages[403]);
            }

            var members = await _memberRepository.GetMembers();

            var projectMembers = members.Where(m => m.ProjectId == projectId);

            var membersDTO = _mapper.Map<List<GetMemberDTO>>(projectMembers);

            return membersDTO;
        }


        [HttpPost]
        public async Task<ActionResult> CreateMember(PostMemberDTO member)
        {
            var project = await _projectRepository.GetProjectById(member.ProjectId);

            if (project == null)
            {
                return NotFound(ErrorMessages.Messages[404]);
            }

            if (!_userAccessService.IsAdmin(project))
            {
                return Unauthorized(ErrorMessages.Messages[403]);
            }

            if(project.Members.Any(m => m.UserId == member.UserId))
            {
                return Conflict(ErrorMessages.Messages[409]);
            }

            var newMember = _mapper.Map<Member>(member);

            _memberRepository.CreateMember(newMember);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditMember(EditMemberDTO member)
        {

            Member updatedMember = await _memberRepository.GetMemberById(member.Id);

            if (updatedMember == null)
            {
                return NotFound(ErrorMessages.Messages[404]);
            }

            var project = await _projectRepository.GetProjectById(updatedMember.ProjectId);

            if (!_userAccessService.IsAdmin(project))
            {
                return Unauthorized(ErrorMessages.Messages[401]);
            }

            var roleInDTO = await _roleRepository.GetRoleByName((MemberRoles)member.Role);

            var existingRole = updatedMember.Roles.FirstOrDefault(r => r.Role.Id == roleInDTO.Id);

            var isMemberOwner = updatedMember.Roles.FirstOrDefault(r => r.Role.Name == MemberRoles.Owner);


            if (existingRole != null)
            {
                if (isMemberOwner != null && roleInDTO.Name == MemberRoles.Admin)
                {
                    return StatusCode(403, "Cannot Remove The Admin Role From The Owner.");

                }
                updatedMember.Roles.Remove(existingRole);
            }
            else
            {
                updatedMember.Roles.Add(new MemberRole { RoleId = roleInDTO.Id });
            }

            _mapper.Map(member, updatedMember);

            _memberRepository.UpdateMember(updatedMember);

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _memberRepository.GetMemberById(id);

            if (member == null)
                return NotFound(ErrorMessages.Messages[404]);

            if (_userAccessService.IsAdmin(member.Project))
            {
                return Forbid(ErrorMessages.Messages[403]);
            }

            _memberRepository.DeleteMember(member);
            return NoContent();
        }

    }
}
