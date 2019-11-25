using ClaimSystem.DAL.CommonRepository;
using ClaimSystem.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClaimSystem.DAL
{
    public class UserRepository :Repository<ApplicationUser>, IUserRepository
    {
        private DbSet<ApplicationUser> _objectSet;
        private readonly ClaimContext _dbContext = new ClaimContext();
        
        public UserRepository()
        {
            _objectSet = _dbContext.Set<ApplicationUser>();
           
        }
        
        public IQueryable<ApplicationUser> SelectUserByEmail(string email)
        {
            var obj = _dbContext.Users.Where(a => a.Email == email);
            return obj;
        }

        
    }
}
