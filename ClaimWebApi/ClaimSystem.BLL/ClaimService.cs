using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClaimSystem.BLL.Mappings;
using ClaimSystem.DAL;
using ClaimSystem.DAL.Entities;
using ClaimSystem.shared.dto;

namespace ClaimSystem.BLL
{
    public class ClaimService
    {
        private readonly IClaimRepository claimRepo;
        private readonly IClaimDetailRepository claimDetailDepo ;

        public ClaimService(IClaimRepository claimRepo, IClaimDetailRepository claimDetailDepo)
        {
            this.claimRepo = claimRepo;
            this.claimDetailDepo = claimDetailDepo;
        }

        // Add reimbursement Claims
        public Task<int> InsertUserClaim(ReimbursementClaimDto dto)
        {
            try
            {
                var entity = AutoMapperConfiguration.Mapper.Map<ReimbursementClaim>(dto);
                return claimRepo.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:UsersBusiness::InsertClaim::Error occured.", ex);
            }
        }

        // Get all user claim details
        public IEnumerable<ReimbursementClaimDto> GetMyUserClaims(string ownerId)
        {
            try
            {
                var res = claimRepo.SelectAll().Where(s => s.ClaimOwnerId.Equals(ownerId));
                var dto = AutoMapperConfiguration.Mapper.Map<IEnumerable<ReimbursementClaimDto>>(res);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:UsersBusiness::GetAllUsers::Error occured.", ex);
            }
        }

        // Get particular user details
        public ReimbursementClaimDto GetUserClaimById(int id)
        {
            try
            {
                var result = claimRepo.SelectById(id);
                 var dto = AutoMapperConfiguration.Mapper.Map<ReimbursementClaimDto>(result);
                 return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:ClaimBusiness::GetClaimById::Error occured.", ex);
            }
        }

        // Check if Claim exists
        public bool ClaimExists(int id)
        {
            return claimRepo.EntityExists(id);
        }


        // EDit the claim
        public int EditUserClaimById(int id, EditReimbursementDto claimDto)
        {
            try
            {
                
                // Find claim to edit in database
                var claimToedit = claimRepo.SelectById(id);
                if (claimToedit != null)
                {
                  
                    claimToedit.Date = claimDto.Date;
                    claimToedit.ReimbursementType = claimDto.ReimbursementType;
                    claimToedit.RequestedValue = claimDto.RequestedValue;
                    claimToedit.Currency = claimDto.Currency;
                }
                
                var result = claimRepo.Update(claimToedit);        
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:ClaimBusiness::EditClaimById::Error occured.", ex);
            }
        }

        // Delete User Claim
        public int RemoveUserClaimById(int id)
        {
            try
            {
                var result = claimRepo.DeleteById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:ClaimBusiness::DeleteClaimById::Error occured.", ex);
            }
        }

        // Get all pending claims
        public IEnumerable<PendingClaimsDto> GetPendingUserClaims()
        {
            var pendList = this.GetAllUserClaimsList().Where(x => !x.IsProcessed && x.Status==null);

            return pendList;
        }

        // Update the claim after approval or decline
        public int editClaimRequest(int id, bool claimStatus)
        {
            var claim = claimRepo.SelectById(id);
            claim.IsProcessed = true;
            claim.Status = claimStatus;
            var result = claimRepo.Update(claim);
            return result;
        }


        // Get all processed Claims
        public IEnumerable<PendingClaimsDto> GetProcessedUserClaims()
        {
            var pendList = this.GetAllUserClaimsList().Where(x => x.IsProcessed);

            return pendList;
        }

        // Get Pending claims
        public List<PendingClaimsDto> GetAllUserClaimsList()
        {
            try
            {
                var claimSet = claimRepo.SelectAllUserClaims().ToArray();

                var query = claimSet.
                    Select((ob) => new
                {
                    ob.ClaimId,
                    ob.Date,
                    ob.ReimbursementType,
                    ob.RequestedValue,
                    ob.Currency,
                    ob.IsProcessed,
                    ob.Status,
                    ob.UploadedFilePath,
                    ob.ClaimOwnerId,
                    ob.ApplicationUser.Email
                });
                
                var pendList = new List<PendingClaimsDto>();
                foreach (var obj in query)
                {
                    var pending = new PendingClaimsDto
                    {
                        ClaimId = obj.ClaimId,
                        Date = obj.Date,
                        ReimbursementType = obj.ReimbursementType,
                        RequestedValue = obj.RequestedValue,
                        IsProcessed = obj.IsProcessed,
                        Currency = obj.Currency,
                        Status = obj.Status,
                        UploadedFilePath = obj.UploadedFilePath,
                        ClaimOwnerEmail = obj.Email
                    };
                    pendList.Add(pending);
                }
               
                return pendList;
                        
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:UsersBusiness::GetAllUsers::Error occured.", ex);
            }
        }

        public IEnumerable<DataDto> GetBarData()
        {
            var setList = claimRepo.SelectAll();
           
            var resultList = setList.GroupBy(t => t.ReimbursementType)
                .Select(x => new { Category = x.Key, Count = x.Count() });
            var List = new List<DataDto>();
            
            foreach (var item in resultList)
            {
                var obj = new DataDto
                {
                    Category = item.Category,
                    Count = item.Count
                };
                List.Add(obj);
            }
            return List;

        }




    }
}
