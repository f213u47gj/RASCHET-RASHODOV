﻿using System.ComponentModel.DataAnnotations;

namespace RASCHET_HASHODOV.ViewModels.forUser
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Новый пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Старый пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
