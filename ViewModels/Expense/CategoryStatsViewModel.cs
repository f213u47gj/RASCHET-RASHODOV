namespace RASCHET_HASHODOV.ViewModels
{
    public class CategoryStatsViewModel
    {
        public string CategoryName { get; set; }
        public decimal TotalSpent { get; set; }  // Общая сумма по категории
        public decimal CurrentMonthSpent { get; set; }  // Сумма по категории за месяц
    }

}
