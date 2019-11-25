using ClaimSystem.DAL.CommonRepository;
using ClaimSystem.DAL.Entities;
using System.Collections.Generic;

namespace ClaimSystem.DAL
{
    public interface IClaimDetailRepository : IRepository<ClaimDetails>
    {
        IEnumerable<ClaimDetails> fetchllData();
    }
}
