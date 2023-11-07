using API_tresure.Models;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _context.AppUsers.FindAsync(userId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
