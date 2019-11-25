using ClaimSystem.DAL.CommonRepository;
using ClaimSystem.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimSystem.DAL
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IQueryable<ApplicationUser> SelectUserByEmail(string email);
    }
}
