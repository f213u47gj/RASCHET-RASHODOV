﻿using System.ComponentModel.DataAnnotations;

namespace RASCHET_HASHODOV.ViewModels.forUser
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Почта обязательна для заполнения")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязательно для заполнения")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Имя пользователя должно быть от 4 до 50 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}