using System.Collections.Generic;
using System.Threading.Tasks;
using RASCHET_HASHODOV.Models;

namespace RASCHET_HASHODOV.Repositories
{
    public interface IExpenseCategoryRepository
    {
        Task<IEnumerable<ExpenseCategory>> GetAllAsync();
        Task<ExpenseCategory> GetByIdAsync(int id);
        Task AddAsync(ExpenseCategory category);
        Task UpdateAsync(ExpenseCategory category);
        Task DeleteAsync(int id);
    }
}
