using API_tresure.Models;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }


        public bool CreateProject(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public bool DeleteProject(Project project)
        {
            _context.Remove(project);
            return Save();
        }

        public async Task<Project> GetProjectById(int projectId)
        {
            return await _context.Projects
            .Include(p => p.Columns)
                .ThenInclude(c => c.Cards)
                    .ThenInclude(c => c.AssignedMembers)
            .Include(p => p.Members)
                .ThenInclude(m => m.Roles)
                    .ThenInclude(r => r.Role)
            .Include(p => p.Members)
                    .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        //TODO remove if unused
        public async Task<ICollection<Column>> GetProjectColumns(int projectId)
        {
            var project = await GetProjectById(projectId);

            return project.Columns.ToList();
        }

        //TODO remove if unused
        public async Task<ICollection<Member>> GetProjectMembers(int projectId)
        {
            var project = await GetProjectById(projectId);

            return project.Members.ToList();
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.Include(p=>p.Members).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateProject(Project project)
        {
            _context.Update(project);
            return Save();
        }
    }
}
