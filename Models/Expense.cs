using System;
using System.ComponentModel.DataAnnotations;

namespace RASCHET_HASHODOV.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int CategoryId { get; set; }
        public virtual ExpenseCategory Category { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
