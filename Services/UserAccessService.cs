using System.Security.Claims;
using API_tresure.Models;
using tresure_api.Data.Enum;
using tresure_api.Data.Interfaces;

namespace API_tresure.Services
{
    public class UserAccessService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public readonly IUserRepository _userRepository;

        public UserAccessService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;

            _userRepository = userRepository;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<User> GetUser()
        {
            var user_id = GetUserId();

            return await _userRepository.GetUserById(user_id);
        }

        public bool IsAdmin(Project project)
        {
            var userId = GetUserId();

            return project.Members.Any(m => m.UserId == userId && m.Roles.Any(r => r.Role.Name == MemberRoles.Admin));
        }
        public bool IsOwner(Project project)
        {
            var userId = GetUserId();

            return project.Members.Any(m => m.UserId == userId && m.Roles.Any(r => r.Role.Name == MemberRoles.Owner));
        }
        public bool IsMember(Project project)
        {
            var userId = GetUserId();

            return project.Members.Any(m => m.UserId == userId);
        }
        public bool IsTaskMaster(Project project)
        {
            var userId = GetUserId();

            return project.Members.Any(m => m.UserId == userId && m.Roles.Any(r => r.Role.Name == MemberRoles.Admin || r.Role.Name == MemberRoles.TaskMaster));
        }

    }
}