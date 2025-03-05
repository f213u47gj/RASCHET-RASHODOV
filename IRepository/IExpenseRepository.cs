using RASCHET_HASHODOV.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RASCHET_HASHODOV.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense> GetByIdAsync(int id);
        Task AddAsync(Expense entity);
        Task UpdateAsync(Expense entity);
        Task DeleteAsync(int id);
    }
}
