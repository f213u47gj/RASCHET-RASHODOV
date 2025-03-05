using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RASCHET_HASHODOV.Models
{
    public class Forecast
    {
        public int Id { get; set; } // Уникальный идентификатор прогноза

        public string UserId { get; set; } // ID пользователя, которому принадлежит прогноз
        public virtual User User { get; set; } // Связь с моделью пользователя

        [Required]
        public decimal PredictedIncome { get; set; } // Прогнозируемый доход пользователя на следующий месяц (например, зарплата, стипендия)

        [Required]
        public decimal PredictedTuitionFees { get; set; } // Прогнозируемые расходы на обучение (например, плата за семестр)

        [Required]
        public decimal PredictedLivingExpenses { get; set; } // Прогнозируемые расходы на проживание (например, аренда жилья)

        [Required]
        public decimal PredictedTransport { get; set; } // Прогнозируемые расходы на транспорт (например, общественный транспорт)

        [Required]
        public decimal PredictedMiscellaneous { get; set; } // Прогнозируемые прочие расходы (например, развлечения, питание вне дома)

        [Required]
        public decimal PredictedTotalExpenses { get; set; } // Общая сумма расходов (обучение, проживание, транспорт и т.д.)

        [Required]
        public decimal PredictedSavings { get; set; } // Прогнозируемая сумма сбережений (доход минус расходы)

        [Required]
        public DateTime ForecastDate { get; set; } // Месяц на который производится прогноз 

        public string ForecastText { get; set; } // Описание прогноза, рекомендации для пользователя (например, "Вам стоит сократить расходы на развлечения, чтобы покрыть расходы на учебу")
    }
}
