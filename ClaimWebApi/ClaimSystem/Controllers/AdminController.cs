using ClaimSystem.BLL;
using ClaimSystem.BLL.Mappings;
using ClaimSystem.DAL.Entities;
using ClaimSystem.shared.dto;
using ClaimSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ClaimSystem.Controllers
{
    public class AdminController : ApiController
    {

        private ClaimService claimService ;
        private ClaimDetailsService claimDetailsService ;

        public AdminController(ClaimService claimService, ClaimDetailsService claimDetailsService)
        {
            this.claimService = claimService;
                this.claimDetailsService = claimDetailsService;
        }

        /// <summary>
        ///  All Admin Related Tasks.
        ///  1. View Pending, Approved and Declined Claims
        ///  2. Approve a request and add Details
        /// </summary>
        /// <returns></returns>
        [Route("api/admin/claim")]
        [HttpGet]
        [ResponseType(typeof(PendingClaimsDto))]
        public IEnumerable<PendingClaimsDto> GetPendingClaims()
        {
            var claimList = claimService.GetPendingUserClaims();
           // var result = AutoMapperConfiguration.Mapper.Map<IEnumerable<ReimbursementClaimView>>(claimList);
         //   return result;
            return claimList;
        }


        [Route("api/admin/claim/Declined")]
        [HttpGet]
        // [Authorize]
        [ResponseType(typeof(PendingClaimsDto))]
        public IEnumerable<PendingClaimsDto> GetDeclinedClaims()
        {
            var processedClaimsList = claimService.GetProcessedUserClaims();
            var declinedClaimList = processedClaimsList.Where(x => x.Status==false);
            // var result = AutoMapperConfiguration.Mapper.Map<IEnumerable<ReimbursementClaimView>>(claimList);
            //   return result;
            return declinedClaimList;
        }

        // APPROVE & DECLINE

        // Update a Claim and set Status to Decline
        [Route("api/admin/claim/Declined/{id}")]
        [HttpPut]
        // [Authorize]
        [ResponseType(typeof(PendingClaimsDto))]
        public IHttpActionResult PutDeclinedClaimRequest(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            try
            {
                // var result = AutoMapperConfiguration.Mapper.Map<ClaimDetailsDto>(student);
                bool isApproved = false;
                int result = claimService.editClaimRequest(id, isApproved);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    
        //  Add Claim Details while Approving a Claim
      
        [Route("api/admin/claim/details/add")]
        [HttpPost]
        // [Authorize]
        [ResponseType(typeof(ClaimDetailsViewModel))]
        public async Task<IHttpActionResult> PostClaimDetails(ClaimDetailsDto claimDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");   
            try
            {
                //var claimDto = AutoMapperConfiguration.Mapper.Map<ClaimDetailsDto>(claimDetails);
                await claimDetailsService.InsertClaimDetail(claimDetails);            
                return Ok(claimDetails);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Get all approved Claims with their claim Details
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("api/admin/claim/details")]
        [HttpGet]
        // [Authorize]
        [ResponseType(typeof(ClaimDetailsDto))]
        public IEnumerable<ClaimDetailsDto> GetApprovedClaims()
        {
            var processedClaimsList = claimDetailsService.GetAllApprovedClaimDetails();
            // var result = AutoMapperConfiguration.Mapper.Map<IEnumerable<ReimbursementClaimView>>(claimList);
            //   return result;
            return processedClaimsList;
        }

       

    }
}
