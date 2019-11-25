using ClaimSystem.DAL.CommonRepository;
using ClaimSystem.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ClaimSystem.DAL
{
    public interface IClaimRepository : IRepository<ReimbursementClaim>
    {
        bool EntityExists(int id);
        IQueryable<ReimbursementClaim> SelectAllUserClaims();
    }
}
