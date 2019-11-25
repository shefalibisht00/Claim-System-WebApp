using ClaimSystem.BLL.Mappings;
using ClaimSystem.DAL;
using ClaimSystem.DAL.Entities;
using ClaimSystem.shared.dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSystem.BLL
{
    public class ClaimDetailsService
    {

        private readonly IClaimRepository claimRepo;
        private readonly IClaimDetailRepository claimDetailRepo;

        public ClaimDetailsService(IClaimRepository claimRepo, IClaimDetailRepository claimDetailRepo)
        {
            this.claimRepo = claimRepo;
            this.claimDetailRepo = claimDetailRepo;
        }
      

        public int EditApproveClaimRequest(int id, bool claimStatus, decimal approvedAmount)
        {
            var claim = claimRepo.SelectById(id);
            claim.IsProcessed = true;
            claim.Status = claimStatus;
            claim.ApprovedValue = approvedAmount;
            var result = claimRepo.Update(claim);
            return result;
        }

        public Task<int> InsertClaimDetail(ClaimDetailsDto dto)
        {
            try
            {
                var entity = AutoMapperConfiguration.Mapper.Map<ClaimDetails>(dto);
                var isValidClaim = ClaimExists(dto.ClaimDetailId);
                if (isValidClaim)
                {
                    // Approving the Claim
                    bool isApproved = true;
                    int result = EditApproveClaimRequest (dto.ClaimDetailId, isApproved, dto.ApprovedAmount);
                    // Inserting the Related Claim Detail
               
                    return claimDetailRepo.Insert(entity);
                }
                throw new Exception("Invald claim Id");
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:ClaimDetailsBusiness::InsertClaim::Error occured.", ex);
            }
        }

        // Get all user details
        public IEnumerable<ClaimDetailsDto> GetAllApprovedClaimDetails()
        {
            try
            {
                var claimDetails = claimDetailRepo.fetchllData();
                var dto = AutoMapperConfiguration.Mapper.Map<IEnumerable<ClaimDetailsDto>>(claimDetails); 
                var approvedclaimList = dto.Where(a => a.ReimbursementClaim.IsProcessed && a.ReimbursementClaim.Status==true);
                return approvedclaimList;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:ClaimDetailsBusiness::GetAllUsers::Error occured.", ex);
            }
        }

        public bool ClaimExists(int id)
        {
            return claimRepo.EntityExists(id);
        }

    }
}
