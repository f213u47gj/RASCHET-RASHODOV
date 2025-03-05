using System.Collections.Generic;
using RASCHET_HASHODOV.Models;

namespace RASCHET_HASHODOV.ViewModels
{
    public class ExpensesViewModel
    {
        public decimal TotalAmount { get; set; } // Общая сумма расходов
        public decimal CurrentMonthAmount { get; set; } // Сумма расходов за текущий месяц
        public List<Expense> Expenses { get; set; }
    }
}
