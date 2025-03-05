using System.ComponentModel.DataAnnotations;

namespace RASCHET_HASHODOV.Models
{
    public class BudgetRecommendation
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string RecommendationText { get; set; }
    }
}
