using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RASCHET_HASHODOV.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
