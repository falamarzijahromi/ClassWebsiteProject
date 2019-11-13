using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Models;
using Infra.Efcore;
using Microsoft.EntityFrameworkCore;

namespace Infra.AspnetcoreIdentity
{
    public class EfCoreUserRepo : IUserRepository
    {
        private readonly ProjectDbContext dbContext;

        public EfCoreUserRepo(ProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<User> Users => dbContext.Users;

        public Task<User> GetUserByIdAsync(string userId)
        {
            var user = dbContext.Users
                .Include(usr => usr.Claims)
                .FirstOrDefault(usr => usr.Id == userId);

            return Task.FromResult(user);
        }

        public async Task<IList<User>> GetUsersByClaim(Claim claim, CancellationToken cancellationToken)
        {
            var users = await dbContext.Users
                .Where(usr =>
                            usr.Claims.Any(clm => clm.Type == claim.Type && clm.Value == claim.Value))
                            .ToListAsync(cancellationToken);

            return users;
        }

        public async Task SaveUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            dbContext.Users.Update(user);

            await dbContext.SaveChangesAsync();
        }
    }
}
