using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteHttp.Models
{
    public class AddressViewModel
    {
        public string City { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
    }
}
