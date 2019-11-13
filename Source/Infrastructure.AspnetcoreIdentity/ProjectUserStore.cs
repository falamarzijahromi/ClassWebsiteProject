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
using System.Security.Claims;

namespace Infra.AspnetcoreIdentity
{
    public class ProjectUserStore : IQueryableUserStore<User>, IUserPasswordStore<User>, IUserClaimStore<User>
    {
        private readonly IUserRepository userRepo;

        public ProjectUserStore(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public IQueryable<User> Users => userRepo.Users;

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            IdentityResult result;

            try
            {
                await userRepo.SaveUserAsync(user);

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

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetUserByIdAsync(userId);

            return user;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetUserByIdAsync(normalizedUserName);

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


        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            var claims = user.Claims.Select(acm => new Claim(acm.Type, acm.Value)).ToList();

            return Task.FromResult((IList<Claim>)claims);
        }

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                var appClaim = new AppClaim { Type = claim.Type, Value = claim.Value };

                user.Claims.Add(appClaim);
            }

            await userRepo.UpdateUser(user);
        }

        public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            await RemoveClaimsAsync(user, new List<Claim> { claim }, cancellationToken);
            await AddClaimsAsync(user, new List<Claim> { newClaim }, cancellationToken);
            await userRepo.UpdateUser(user);
        }

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            var removeCandidate =
                from clm in claims
                join appclm in user.Claims on new { clm.Type, clm.Value } equals new { appclm.Type, appclm.Value }
                select appclm;

            foreach (var appClaim in removeCandidate)
            {
                user.Claims.Remove(appClaim);
            }

            await userRepo.UpdateUser(user);

        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return await userRepo.GetUsersByClaim(claim, cancellationToken);
        }
    }
}
