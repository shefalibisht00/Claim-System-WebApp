using ClaimSystem.BLL;
using ClaimSystem.BLL.Mappings;
using ClaimSystem.DAL;
using ClaimSystem.DAL.Entities;
using ClaimSystem.shared.dto;
using ClaimSystem.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Security.Claims;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ClaimSystem.Controllers
{
    public class ClaimController : ApiController
    {
        private ClaimService claimService;

        public ClaimController(ClaimService claimService)
        {
            this.claimService = claimService;
        }

        /// <summary>
        ///  Controller for GET, POST, PUT, DELETE User Reimbursement Claims
        /// </summary>
        /// <returns></returns>


        [Route("api/Claims")]
        [HttpGet]
       // [Authorize]
        [ResponseType(typeof(ReimbursementClaimDto))]
        public IEnumerable<ReimbursementClaimDto> GetUserClaims()
        {
            // Get loggedIn userId via IdentityClaims
            
            var identityClaims = (ClaimsIdentity)User.Identity;
            var claimOwnerId = identityClaims.FindFirst("Id").Value;
   
            var claimList = claimService.GetMyUserClaims(claimOwnerId);
            //var result = AutoMapperConfiguration.Mapper.Map<IEnumerable<ReimbursementClaimView>>(claimList);
            //return result;
            return claimList;
        }

        [HttpGet]
        [Route("api/Claim/RecieptImage")]
        public HttpResponseMessage RecieptImage(string ImageName)
        {
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Uploads/" + ImageName), FileMode.Open);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(fs);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return response;
        }

        [HttpPost]
        [Route("api/Claim/Add")]
        public async Task<IHttpActionResult> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
           
            
            // Local Path Storage
            string root = HttpContext.Current.Server.MapPath("~/App_Data/Uploads");
            var provider = new MultipartFormDataStreamProvider(root);      
            try
            {
              //  Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
    
                string imageName = null;
                var httpRequest = HttpContext.Current.Request;
                ////Upload Image
                var postedFile = httpRequest.Files["UploadImage"];


                ////Create custom filename
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);

                var filePath = HttpContext.Current.Server.MapPath("~/App_Data/Uploads/" + imageName);
                postedFile.SaveAs(filePath);

                var claimData = new ReimbursementClaimDto
                {

                    Date = DateTime.Parse(provider.FormData.GetValues("Date").SingleOrDefault()),
                    ReimbursementType = provider.FormData.GetValues("ReimbursementType").SingleOrDefault(),
                    RequestedValue = Convert.ToDecimal(provider.FormData.GetValues("RequestedValue").SingleOrDefault()),              
                    ApprovedValue = 0,
                    Currency = provider.FormData.GetValues("Currency").SingleOrDefault(),
                    IsProcessed = false,
                    Status = null,
                    ClaimOwnerId = provider.FormData.GetValues("ClaimOwnerId").SingleOrDefault(),
                    UploadedFilePath = imageName,
                };
                await claimService.InsertUserClaim(claimData);
                return Ok(claimData);
              //  return CreatedAtRoute("DefaultApi", new { id = claimData.ClaimId}, claimData);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
                        
        }

       // Get Reimbursement Claims made by Claim owner
        [Route("api/Claim/{id}")]
        [HttpGet]
        [ResponseType(typeof(ReimbursementClaimDto))]
        public IHttpActionResult GetClaim(int id)
        {
            if (id<=0)
            {
                return BadRequest("Not a valid Claim id");
            }
            var claim = claimService.GetUserClaimById(id);
            if (claim == null)
            {
                return NotFound();
            }
         //   var result = AutoMapperConfiguration.Mapper.Map<ReimbursementClaimView>(claim);
            return Ok(claim);
        }


        // PUT:  Edit Reimbursement Claims
        [Route("api/Claim/{id}")]
        [HttpPut]
        [ResponseType(typeof(EditReimbursementView))]
        public IHttpActionResult PutClaim(int id, EditReimbursementView editClaim)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != editClaim.ClaimId)
            {
                return BadRequest("Not a valid Claim id"+  id + editClaim.ClaimId);
            }
            try{
                var editClaimDto = new EditReimbursementDto()
                {
                    Date = editClaim.Date,
                    ReimbursementType= editClaim.ReimbursementType,
                    RequestedValue = editClaim.RequestedValue,
                    Currency = editClaim.Currency,                
                };
                var updatedClaim = claimService.EditUserClaimById(id, editClaimDto);
                return StatusCode(HttpStatusCode.OK);
              }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:ClaimBusiness::EditClaimById::Error occured.", ex);
            }     
        }


        // DELETE:  Reimbursement Claims made by user
        [Route("api/Claim/{id}")]
        [HttpDelete]
        [ResponseType(typeof(ReimbursementClaim))]
        public IHttpActionResult DeleteUserClaim(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Claim id");
            var claim = claimService.GetUserClaimById(id);
            if (claim == null)
            {
                return NotFound();
            }
            try
            {
                int sucesss = claimService.RemoveUserClaimById(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Unable to delete"); ;
            }       
        }

        // **************************
        [Route("api/claim/chart")]
        [HttpGet]
        public IEnumerable<DataDto> GetBarData()
        {
            var result = claimService.GetBarData();
            return result;
        }


    }
}
