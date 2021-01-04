using Hahn.ApplicatonProcess.December2020.Domain;
using Hahn.ApplicatonProcess.December2020.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUtilities _utilities;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUtilities utilities)
        {
            _logger = logger;
            _utilities = utilities;
        }

        [HttpPost]
        public async Task<IActionResult> Validate()
        {
            var applicantDTO = new SmileVerificationDto();
            applicantDTO.IdNumber = "A00000000";
            applicantDTO.IdType = IdType.NIN;
            applicantDTO.Country = Country.NG;
            applicantDTO.PartnerParams = new PartnerDataDto
            {
                JobId = "5fdb68a584ea55001b78cffb",
                JobType = 5,
                UserId = "2f3857d7-1996-451b-b3aa-ec2a1be59095"
            };

            var response = await _utilities.MakeHttpRequest(applicantDTO, "https://localhost:5006/smile/verify", "", HttpMethod.Post);

            if(response.IsSuccessStatusCode)
            {

            }
            else
            {

            }

            return Ok(applicantDTO);
        }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SmileVerificationDto
    {
        public IdType IdType { get; set; }
        public Country Country { get; set; }
        public DateTime Dob { get; set; }
        public String IdNumber { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String PhoneNumber { get; set; }
        public PartnerDataDto PartnerParams { get; set; }

    }

    public class PartnerDataDto
    {
        public String UserId { get; set; }
        public String JobId { get; set; }
        public int JobType { get; set; }
    }
    public enum IdType
    {
        PASSPORT,
        BVN,
        NIN,
        NIN_SLIP,
        DRIVERS_LICENSE,
        VOTER_ID,
        NATIONAL_ID,
        CAC,
        TIN
    }
    public enum Country
    {
        GH, NG, KE, ZA
    }
}
