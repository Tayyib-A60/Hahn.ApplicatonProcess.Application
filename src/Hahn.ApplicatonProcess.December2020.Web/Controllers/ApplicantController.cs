using Hahn.ApplicatonProcess.December2020.Domain;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        /// <summary>
        /// Adds a new applicant record to the database
        /// </summary>
        /// <response code="200">Creates a new applicant</response>
        /// <response code="400">Unable to create applicant due to validation error(s)</response>
        /// <response code="500">Other error(s)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApplicantDTO), 201)]
        public async Task<IActionResult> NewApplicant([FromBody] ApplicantDTO applicantDTO)
        {
            var (applicantCreated, errorMessage) = await _applicantService.CreateNewApplicant(applicantDTO);

            if (String.IsNullOrEmpty(errorMessage))
            {
                return new ObjectResult(applicantCreated)
                {
                    StatusCode = 201
                };
            }

            return new ObjectResult(new { message = errorMessage })
            {
                StatusCode = 500
            };
        }

        /// <summary>
        /// Updates an existing applicant record in the database
        /// </summary>
        /// <response code="200">Update successful</response>
        /// <response code="400">Unable to update applicant due to validation error(s)</response>
        /// <response code="404">Applicant with id not found</response>
        /// <response code="500">Other error(s)</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApplicantDTO), 200)]
        public async Task<IActionResult> UpdateApplicant(string id, ApplicantDTO applicantDTO)
        {
            if(id.ToUpper() != applicantDTO.ID.ToString().ToUpper())
            {
                return new ObjectResult(new { message = "ID must be the same as applicant object Id" })
                {
                    StatusCode = 400
                };
            }
            var (updatedApplicant, errorMessage) = await _applicantService.UpdateApplicant(id, applicantDTO);

            if (String.IsNullOrEmpty(errorMessage))
            {
                return new ObjectResult(updatedApplicant)
                {
                    StatusCode = 200
                };
            }

            return new ObjectResult(new { message = errorMessage })
            {
                StatusCode = errorMessage.ToLower().Contains("not found") ? 404 : 500
            };
        }

        /// <summary>
        /// Retrieves an existing applicant record from the database
        /// </summary>
        /// <response code="200">Retrieval successful</response>
        /// <response code="404">Applicant with id not found</response>
        /// <response code="500">Other error(s)</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicantDTO), 200)]
        public async Task<IActionResult> GetApplicant(string id)
        {
            var (applicant, errorMessage) = await _applicantService.GetApplicant(id);

            if(String.IsNullOrEmpty(errorMessage) && applicant != null)
            {
                return new ObjectResult(applicant)
                {
                    StatusCode = 200
                };
            }

            return new ObjectResult(new { message = errorMessage })
            {
                StatusCode = errorMessage.ToLower().Contains("not found") ? 404 : 500
            };
        }

        /// <summary>
        /// Removes an existing applicant record from the database
        /// </summary>
        /// <response code="200">Delete successful</response>
        /// <response code="404">Applicant with id not found</response>
        /// <response code="500">Other error(s)</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeleteApplicant(string id)
        {
            var (ID, errorMessage) = await _applicantService.DeleteApplicant(id);

            if (String.IsNullOrEmpty(errorMessage))
            {
                return new ObjectResult(ID)
                {
                    StatusCode = 200
                };
            }

            return new ObjectResult(new { message = errorMessage })
            {
                StatusCode = errorMessage.ToLower().Contains("not found") ? 404 : 500
            };
        }
    }
}
