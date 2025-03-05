using System.ComponentModel.DataAnnotations;

namespace RASCHET_HASHODOV.Models
{
    public class Budget
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Доход должен быть положительным")]
        public decimal PlannedIncome { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Расходы должны быть положительными")]
        public decimal PlannedExpenses { get; set; }
    }
}
