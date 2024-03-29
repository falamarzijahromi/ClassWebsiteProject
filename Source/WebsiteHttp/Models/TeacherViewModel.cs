using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteHttp.Models
{
    public class TeacherViewModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string MeliCode { get; set; }
        public string PersonalCode { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
