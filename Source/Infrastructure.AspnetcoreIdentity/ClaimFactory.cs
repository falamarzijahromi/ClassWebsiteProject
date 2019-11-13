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

            var identity = defaultPrincipal.Identities.Single();

            var role = dbContext.Roles
                .SingleOrDefault(rl =>
                rl.Users.Any(usr => usr.Id == user.Id));

            if (role != null)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role.Id);

                identity.AddClaim(roleClaim);
            }

            return defaultPrincipal;
        }
    }
}
