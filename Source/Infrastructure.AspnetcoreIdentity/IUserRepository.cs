using Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.AspnetcoreIdentity
{
    public interface IUserRepository
    {
        Task SaveUserAsync(User user);

        Task<User> GetUserByIdAsync(string userId);

        IQueryable<User> Users { get; }
    }
}
