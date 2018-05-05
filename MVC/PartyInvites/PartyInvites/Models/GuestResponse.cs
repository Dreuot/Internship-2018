using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Пожалуйста, введите Ваше имя.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите email адрес")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Пожалуйста, введите валидный email адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите Ваш номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите, придете Вы или нет")]
        public bool? WillAttend { get; set; }
    }
}