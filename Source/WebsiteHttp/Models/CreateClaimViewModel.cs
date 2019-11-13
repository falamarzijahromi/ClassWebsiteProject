using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WebsiteHttp.Models
{
    public class CreateClaimViewModel
    {
        public string User { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
