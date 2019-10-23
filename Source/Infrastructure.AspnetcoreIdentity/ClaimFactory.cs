using Common.Models;
using Infra.Efcore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Infra.AspnetcoreIdentity
{
    public class ClaimFactory : IUserClaimsPrincipalFactory<User>
    {
        private readonly UserClaimsPrincipalFactory<User> defaultFactory;
        private readonly ProjectDbContext dbContext;

        public ClaimFactory(UserClaimsPrincipalFactory<User> defaultFactory, ProjectDbContext dbContext)
        {
            this.defaultFactory = defaultFactory;
            this.dbContext = dbContext;
        }

        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var defaultPrincipal = await defaultFactory.CreateAsync(user);

            var defaultClaims = defaultPrincipal.Claims;

            var role = dbContext.Roles
                .SingleOrDefault(rl =>
                rl.Users.Any(usr => usr.Id == user.Id));

            var resultIds = new ClaimsIdentity();

            resultIds.AddClaims(defaultClaims);

            if (role != null)
            {

                var roleClaim = new Claim(ClaimTypes.Role, role.Id);

                resultIds.AddClaim(roleClaim);
            }

            var result = new ClaimsPrincipal(resultIds);

            return result;
        }
    }
}
