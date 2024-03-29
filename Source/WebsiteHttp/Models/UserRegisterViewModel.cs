﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteHttp.Models
{
    public class UserRegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare(nameof(Password))]
        public string ReEnterPassword { get; set; }
        [Required]
        public string NationalCode { get; set; }
    }
}
