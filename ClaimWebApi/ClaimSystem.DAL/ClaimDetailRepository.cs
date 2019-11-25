using ClaimSystem.DAL.CommonRepository;
using ClaimSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClaimSystem.DAL
{
    public class ClaimDetailRepository : Repository<ClaimDetails>, IClaimDetailRepository
    {
        private readonly ClaimContext _dbContext;
        private DbSet<ClaimDetails> _objectSet;

        public ClaimDetailRepository()
        {
            _dbContext = new ClaimContext();
            _objectSet = _dbContext.Set<ClaimDetails>();                
        }

        public IEnumerable<ClaimDetails> fetchllData()
        {
            var a = _objectSet.Include(p => p.ReimbursementClaim).ToList();

            return a;
        }
        // New Added Methods
        public bool EntityExists(int id)
        {
            return _dbContext.ClaimDetails.Count(e => e.ClaimDetailId == id) > 0;
        }

    }
}
