using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WebsiteHttp.DataAnnotations
{
    public class RequiredLengthAttribute : Attribute, IModelValidator
    {
        private readonly int maxCharacters;

        public RequiredLengthAttribute(int maxCharacters)
        {
            this.maxCharacters = maxCharacters;
        }

        public int MinCharacters { get; set; } = -1;

        public bool CanTrim { get; set; } = true;

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var results = new List<ModelValidationResult>();

            var input = context.Model.ToString();

            if (CanTrim)
                input = input.Trim();

            CheckMaxChars(context, results, input);
            CheckMinChars(context, results, input);

            return results;
        }

        private void CheckMinChars(ModelValidationContext context, List<ModelValidationResult> results, string input)
        {
            if (input.Length < MinCharacters)
            {
                var error = new ModelValidationResult(context.ModelMetadata.PropertyName, $"{input} characters is smaller than required minimum: {MinCharacters}");

                results.Add(error);
            }
        }

        private void CheckMaxChars(ModelValidationContext context, List<ModelValidationResult> results, string input)
        {
            if (input.Length > maxCharacters)
            {
                var error = new ModelValidationResult(context.ModelMetadata.PropertyName, $"{input} exceeded maximum characters: {maxCharacters}");

                results.Add(error);
            }
        }
    }
}