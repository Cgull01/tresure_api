using API_tresure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectRepository.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _projectRepository.GetProjects();

            return projects.ToList();
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(PostProjectDTO project)
        {

            var newProject = new Project(){
                Title = project.title,
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
