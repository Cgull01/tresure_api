using API_tresure.Models;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Enum;
using tresure_api.Data.Interfaces;

namespace tresure_api.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.AppRoles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> GetRoleByName(MemberRoles roleName)
        {
            return await _context.AppRoles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<ICollection<Role>> GetRoles()
        {
            return await _context.AppRoles.ToListAsync();
        }
    }
}
