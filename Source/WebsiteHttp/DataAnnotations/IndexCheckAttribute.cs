using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WebsiteHttp.DataAnnotations
{
    public class IndexCheckAttribute : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var result = new List<ModelValidationResult>();

            if (context.ActionContext.ActionDescriptor.DisplayName.ToLower().Contains("index"))
            {
                //.....
            }

            return result;
        }
    }
}