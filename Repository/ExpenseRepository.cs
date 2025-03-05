using Microsoft.EntityFrameworkCore;
using RASCHET_HASHODOV.Data;
using RASCHET_HASHODOV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RASCHET_HASHODOV.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .Include(e => e.Category) // Включаем категорию для загрузки вместе с расходом
                .Include(e => e.User)     // Включаем пользователя для загрузки вместе с расходом
                .ToListAsync();
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _context.Expenses
                .Include(e => e.Category) // Включаем категорию
                .Include(e => e.User)     // Включаем пользователя
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Expense entity)
        {
            await _context.Expenses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense entity)
        {
            _context.Expenses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Expenses.FindAsync(id);
            if (entity != null)
            {
                _context.Expenses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
