using System.Security.Claims;
using API_tresure.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProjectDTO>> GetProject(int id)
        {

            var project = await _projectRepository.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            var projectDTO = _mapper.Map<GetProjectDTO>(project);

            return projectDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            //  var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var projects = await _projectRepository.GetProjects();

            return projects.ToList();
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(PostProjectDTO project)
        {

            var user_id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var newProject = new Project(){
                Title = project.title,
                Columns = new List<Column>() { new Column { Title = "To Do", Position = 0 }, new Column { Title = "Doing", Position = 1 }, new Column { Title = "Done", Position = 2 } },
                Members = new List<Member>(){ new Member{UserId = user_id, Roles = new List<Role>(){ new Role {Name = MemberRole.Admin}}}}
            };

            _projectRepository.CreateProject(newProject);

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> EditProject(PutProjectDTO project)
        {
            Project updatedProject = await _projectRepository.GetProjectById(project.Id);

            if(updatedProject == null)
            {
                return NotFound();
            }

            updatedProject.Title = project.title;

            _projectRepository.UpdateProject(updatedProject);

            return StatusCode(200);
        }
    }
}
