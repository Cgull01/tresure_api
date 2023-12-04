using System.Security.Claims;
using API_tresure.Models;
using API_tresure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using tresure_api.Controllers.Hubs;
using tresure_api.Data;
using tresure_api.Data.Enum;
using tresure_api.Data.Interfaces;

namespace tresure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly UserAccessService _userAccessService;
        private readonly IRoleRepository _roleRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IHubContext<ProjectHub> _hubContext;

        public ProjectController(IProjectRepository projectRepository, IRoleRepository roleRepository, IMemberRepository memberRepository, IMapper mapper, UserAccessService userAccessService, IHubContext<ProjectHub> hubContext)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _userAccessService = userAccessService;
            _roleRepository = roleRepository;
            _hubContext = hubContext;
            _memberRepository = memberRepository;
        }

        [HttpPost("UpdateProject")]
        public async Task<IActionResult> UpdateProject()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveProjectUpdate");
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProjectDTO>> GetProject(int id)
        {
            var project = await _projectRepository.GetProjectById(id);

            if (project == null)
            {
                return NotFound(ErrorMessages.Messages[404]);
            }

            if (!_userAccessService.IsMember(project))
            {
                return Forbid(ErrorMessages.Messages[403]);
            }

            var projectDTO = _mapper.Map<GetProjectDTO>(project);

            return projectDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProjectsDTO>>> GetProjects()
        {

            var userId = _userAccessService.GetUserId();
            var projects = await _projectRepository.GetProjects();
            var userProjects = projects.Where(p => p.Members.Any(m => m.UserId == userId));

            var projectsDTO = new List<GetProjectsDTO>();

            foreach(var project in userProjects)
            {
                projectsDTO.Add(new GetProjectsDTO{Id = project.Id, Title = project.Title, UserAsMember = project.Members.FirstOrDefault(m => m.UserId == userId)});
            }

            return projectsDTO;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(PostProjectDTO project)
        {
            var user_id = _userAccessService.GetUserId();

            var adminRole = await _roleRepository.GetRoleByName(MemberRoles.Admin);
            var ownerRole = await _roleRepository.GetRoleByName(MemberRoles.Owner);

            var newMember = new Member
            {
                UserId = user_id,
                Roles = new List<MemberRole> { new MemberRole { RoleId = adminRole.Id }, new MemberRole { RoleId = ownerRole.Id } }
            };

            var newProject = new Project()
            {
                Title = project.Title,
                Columns = new List<Column>() { new Column { Title = "To Do", Position = 0 }, new Column { Title = "Doing", Position = 1 }, new Column { Title = "Done", Position = 2 } },
                Members = new List<Member>() { newMember }
            };

            _projectRepository.CreateProject(newProject);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditProject(int id, [FromQuery] string projectTitle)
        {
            Project updatedProject = await _projectRepository.GetProjectById(id);

            if (updatedProject == null)
            {
                return NotFound(ErrorMessages.Messages[404]);
            }

            if (!_userAccessService.IsAdmin(updatedProject))
            {
                return Forbid(ErrorMessages.Messages[403]);
            }

            updatedProject.Title = projectTitle;

            _projectRepository.UpdateProject(updatedProject);

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectById(id);

            if (project == null)
                return NotFound(ErrorMessages.Messages[404]);

            if (_userAccessService.IsAdmin(project))
            {
                // if user is owner of the project, delete the project
                _projectRepository.DeleteProject(project);
            }
            else if(_userAccessService.IsMember(project))
            {
                // if user is member of the project, remove user from members
                var userId = _userAccessService.GetUserId();
                var userAsMember = project.Members.FirstOrDefault(m => m.UserId == userId);

                if(userAsMember != null)
                {
                    _memberRepository.DeleteMember(userAsMember);
                }
                else
                {
                    return Forbid(ErrorMessages.Messages[403]);
                }

            }
            else
            {
                return Forbid(ErrorMessages.Messages[403]);
            }

            return NoContent();
        }

    }
}
