using ClaimSystem.DAL.CommonRepository;
using ClaimSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimSystem.DAL
{
    public class ClaimRepository : Repository<ReimbursementClaim>, IClaimRepository
    {
        private DbSet<ReimbursementClaim> _objectSet;
        private readonly ClaimContext _dbContext = new ClaimContext();

        public ClaimRepository()
        {
            _objectSet = _dbContext.Set<ReimbursementClaim>();
        }

        public IQueryable <ReimbursementClaim> SelectAllUserClaims()
        {
            var obj = _objectSet.Include(o => o.ApplicationUser);
            return obj;
        }
        // New Added Methods
        public bool EntityExists(int id)
        {
            return _dbContext.ReimbursementClaim.Count(e => e.ClaimId == id) > 0;
        }

        
    }
}
