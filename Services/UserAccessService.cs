using System.Security.Claims;
using API_tresure.Models;
using tresure_api.Data.Enum;
using tresure_api.Data.Interfaces;

namespace API_tresure.Services
{
    public class UserAccessService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleRepository _roleRepository;

        public UserAccessService(IHttpContextAccessor httpContextAccessor, IRoleRepository roleRepository)
        {
            _httpContextAccessor = httpContextAccessor;

            _roleRepository = roleRepository;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public bool IsOwner(Project project)
        {
            var userId = GetUserId();

            return project.Members.Any(m => m.UserId == userId && m.Roles.Any(r => r.Role.Name == MemberRoles.Admin));
        }
        public bool isMember(Project project)
        {
            var userId = GetUserId();

            return project.Members.Any(m => m.UserId == userId && m.Roles.Any(r => r.Role.Name == MemberRoles.Member || r.Role.Name == MemberRoles.Admin || r.Role.Name == MemberRoles.TaskMaster));
        }
        public bool isTaskMaster(Project project)
        {
            var userId = GetUserId();

            return project.Members.Any(m => m.UserId == userId && m.Roles.Any(r => r.Role.Name == MemberRoles.Admin || r.Role.Name == MemberRoles.TaskMaster));
        }

    }
}