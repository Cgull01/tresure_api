﻿using System.Security.Claims;
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
        private readonly IMapper _mapper;
        private readonly UserAccessService _userAccessService;

        public MemberController(IMapper mapper, UserAccessService userAccessService, IMemberRepository memberRepository, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _userAccessService = userAccessService;
            _memberRepository = memberRepository;
            _projectRepository = projectRepository;
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

            var project =await _projectRepository.GetProjectById(projectId);

            if(!project.Members.Any(m => m.UserId == user_id))
            {
                return StatusCode(401, "Requester Is Not A Member Of The Project");
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
                return NotFound();
            }

            if (!_userAccessService.IsOwner(project))
            {
                return Unauthorized();
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
                return NotFound();
            }

            if (!_userAccessService.IsOwner(updatedMember.Project))
            {
                return NotFound();
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
                return NotFound();

            if (_userAccessService.IsOwner(member.Project))
            {
                return Unauthorized();
            }

            _memberRepository.DeleteMember(member);
            return NoContent();
        }

    }
}
