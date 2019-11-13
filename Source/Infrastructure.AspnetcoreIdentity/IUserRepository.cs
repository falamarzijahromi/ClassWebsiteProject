using Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infra.AspnetcoreIdentity
{
    public interface IUserRepository
    {
        Task SaveUserAsync(User user);

        Task<User> GetUserByIdAsync(string userId);

        Task<IList<User>> GetUsersByClaim(Claim claim, System.Threading.CancellationToken cancellationToken);

        Task UpdateUser(User user);

        IQueryable<User> Users { get; }
    }
}
