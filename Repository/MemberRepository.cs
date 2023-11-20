using API_tresure.Models;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateMember(Member member)
        {
            _context.Add(member);
            return Save();
        }

        public bool DeleteMember(Member member)
        {
            _context.Remove(member);
            return Save();
        }

        public async Task<Member> GetMemberById(int memberId)
        {
            return await _context.Members.Include(m => m.Project).FirstOrDefaultAsync(m => m.Id == memberId);
        }

        public async Task<Member> GetMemberByUserId(string id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.UserId == id);
        }

        public async Task<ICollection<Member>> GetMembers()
        {
            return await _context.Members.Include(m => m.User).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateMember(Member member)
        {
            _context.Update(member);
            return Save();
        }
    }
}
