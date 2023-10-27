using System.Security.Claims;
using API_tresure.Models;
using tresure_api.Data.Enum;

namespace API_tresure.Services
{
    public class UserAccessService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public bool IsOwner(Project project)
        {
            var userId = GetUserId();
            return project.Members.Any(m => m.UserId == userId && m.Roles.Any(r => r.Name == MemberRole.Admin));
        }
    }
}