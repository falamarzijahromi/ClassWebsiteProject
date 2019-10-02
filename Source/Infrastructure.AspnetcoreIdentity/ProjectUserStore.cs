using Common.Models;
using Infra.Efcore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.AspnetcoreIdentity
{
    public class ProjectUserStore : IQueryableUserStore<User>, IUserPasswordStore<User>
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IPasswordHasher<User> _passHasher;

        public ProjectUserStore(
            ProjectDbContext dbContext, 
            IPasswordHasher<User> passHasher, 
            IOptions<CustomOptions> options)
        {
            this._dbContext = dbContext;
            _passHasher = passHasher;
        }

        public IQueryable<User> Users => _dbContext.Users;

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            IdentityResult result;

            try
            {
                var passHash = _passHasher.HashPassword(user, user.Password);

                await SetPasswordHashAsync(user, passHash, cancellationToken);

                await _dbContext.Users.AddAsync(user, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                result = IdentityResult.Success;
            }
            catch (Exception e)
            {
                var error = new IdentityError
                {
                    Description = e.Message,
                };

                result = IdentityResult.Failed(error);
            }

            return result;
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = _dbContext.Users.SingleOrDefault(usr => usr.Id == userId);

            return Task.FromResult(user);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(usr => usr.Id == normalizedUserName, cancellationToken: cancellationToken);

            return user;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Id = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;

            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
