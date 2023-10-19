using API_tresure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace tresure_api.Data.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();

        Task<ICollection<Member>> GetProjectMembers(int projectId);
        Task<ICollection<Column>> GetProjectColumns(int projectId);
        Task<Project> GetProjectById(int id);
        bool CreateProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(Project project);
        bool Save();

    }
}
