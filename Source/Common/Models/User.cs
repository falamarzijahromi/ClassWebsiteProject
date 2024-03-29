﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string NationalCode { get; set; }
        public ICollection<AppClaim> Claims { get; set; }
    }
}
