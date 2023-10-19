using API_tresure.Models;
using Microsoft.EntityFrameworkCore;
using tresure_api.Data;
using tresure_api.Data.Interfaces;

namespace tresure_api.Repository
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly AppDbContext _context;

        public ColumnRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateColumn(Column column)
        {
            _context.Add(column);
            return Save();
        }

        public bool DeleteColumn(Column column)
        {
            _context.Remove(column);
            return Save();
        }

        public async Task<Column> GetColumnById(int columnId)
        {
            return await _context.Columns.Include(c=> c.Cards).FirstOrDefaultAsync(c => c.Id == columnId);
        }

        public async Task<ICollection<Column>> GetColumns()
        {
            return await _context.Columns.Include(c=> c.Cards).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateColumn(Column column)
        {
            _context.Update(column);
            return Save();
        }
    }
}
