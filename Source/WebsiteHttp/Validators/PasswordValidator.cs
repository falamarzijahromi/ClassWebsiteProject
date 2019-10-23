using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteHttp.Validators
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        private readonly IOptions<IdentityOptions> options;

        public PasswordValidator(IOptions<IdentityOptions> options)
        {
            this.options = options;
        }

        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            IdentityResult result;

            if (password.Length >= options.Value.Password.RequiredLength)
            {
                result = IdentityResult.Success;
            }
            else
            {
                var error = new IdentityError() { Description = $"Password length must be over {options.Value.Password.RequiredLength} characters." };

                result = IdentityResult.Failed(error);
            }

            return Task.FromResult(result);
        }
    }
}
