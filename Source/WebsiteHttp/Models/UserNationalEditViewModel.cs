using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteHttp.Models
{
    public class UserNationalEditViewModel
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        [Required]
        public string NationalCode { get; set; }
    }
}
