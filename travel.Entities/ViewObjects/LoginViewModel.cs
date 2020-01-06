using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace travel_web.Entities.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("Username"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Username { get; set; }

        [DisplayName("Password"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { get; set; }
    }
}