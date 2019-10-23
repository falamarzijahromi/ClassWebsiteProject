using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Models;
using Infra.Efcore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infra.AspnetcoreIdentity
{
    public class ProjectRoleStore : IQueryableRoleStore<Role>
    {
        private readonly ProjectDbContext dbContext;

        public IQueryable<Role> Roles => dbContext.Roles.Include(rl => rl.Users);

        public ProjectRoleStore(ProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            IdentityResult result;

            try
            {
                await dbContext.Roles.AddAsync(role);

                await dbContext.SaveChangesAsync();

                result = IdentityResult.Success;
            }
            catch (Exception e)
            {
                var error = new IdentityError() { Description = e.Message };

                result = IdentityResult.Failed(error);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            IdentityResult result;

            try
            {
                dbContext.Roles.Update(role);

                await dbContext.SaveChangesAsync();

                result = IdentityResult.Success;
            }
            catch (Exception e)
            {
                var error = new IdentityError() { Description = e.Message };

                result = IdentityResult.Failed(error);
            }

            return result;
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Id = roleName;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Id = normalizedName;

            return Task.CompletedTask;
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var role = dbContext.Roles.Include(rl => rl.Users).FirstOrDefault(rol => rol.Id == roleId);

            return Task.FromResult(role);
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return FindByIdAsync(normalizedRoleName, cancellationToken);
        }
    }
}
