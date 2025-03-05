using System;
using System.ComponentModel.DataAnnotations;

namespace RASCHET_HASHODOV.Models
{
    public class ExpenseReport
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string Period { get; set; } // Например, "Март 2024"

        [Required]
        public decimal TotalAmount { get; set; }
    }
}
