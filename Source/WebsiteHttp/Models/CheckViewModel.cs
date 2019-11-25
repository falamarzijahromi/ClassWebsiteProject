using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;
using WebsiteHttp.DataAnnotations;

namespace WebsiteHttp.Models
{
    public class CheckViewModel
    {
        [IndexCheck]
        [RequiredLength(maxCharacters: 10, MinCharacters = 5, CanTrim = true)]
        public string Input { get; set; }
    }
}