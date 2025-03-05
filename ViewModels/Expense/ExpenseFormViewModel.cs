using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RASCHET_HASHODOV.Models;

namespace RASCHET_HASHODOV.ViewModels
{
    public class ExpenseFormViewModel
    {
        public int? Id { get; set; } // ID расхода (нужен при редактировании)

        [Required(ErrorMessage = "Введите сумму")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0")]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Выберите дату")]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Выберите категорию")]
        public int CategoryId { get; set; } // ID выбранной категории

        public List<ExpenseCategory> Categories { get; set; } // Доступные категории расходов

        public string UserId { get; set; } // ID пользователя (не показывается в форме)
    }
}
