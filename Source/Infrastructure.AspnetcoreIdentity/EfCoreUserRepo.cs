using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .FirstOrDefault(usr => usr.Id == userId);

            return Task.FromResult(user);
        }

        public async Task SaveUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();
        }
    }
}
