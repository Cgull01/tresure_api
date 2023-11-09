using API_tresure.Models;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateCard(Card card)
        {
             _context.Add(card);
            return Save();
        }

        public bool DeleteCard(Card card)
        {
            _context.Remove(card);
            return Save();
        }

        // If I don't fetch whole column the ownership check fails
        public async Task<Card> GetCardById(int cardId)
        {
            return await _context.Cards.Include(c=>c.AssignedMembers)
                .Include(c=>c.Column)
                    .ThenInclude(c => c.Project)
                        .ThenInclude(p => p.Members)
                            .ThenInclude(m => m.Roles)
                                .ThenInclude(m => m.Role)
                .FirstOrDefaultAsync(c => c.Id == cardId);
        }

        public async Task<ICollection<Card>> GetCards()
        {
            return await _context.Cards.Include(c=>c.AssignedMembers)
                    .Include(c => c.Column)
                    .ThenInclude(c => c.Project)
                    .ThenInclude(p => p.Members)
                    .ThenInclude(m => m.Roles).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateCard(Card card)
        {
            _context.Update(card);
            return Save();
        }
    }
}
