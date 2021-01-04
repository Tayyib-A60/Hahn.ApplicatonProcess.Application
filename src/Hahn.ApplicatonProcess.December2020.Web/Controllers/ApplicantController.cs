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
    public class ApplicantController: ControllerBase
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        /// <summary>
        /// Adds a new applicant recored to the database
        /// </summary>
        /// <response code="200">Creates a new applicant</response>
        /// <response code="400">Unable to create applicant due to validation error(s)</response>
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

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(new { Object = "Value" })
            {
                StatusCode = 201
            };
        }
    }
}
