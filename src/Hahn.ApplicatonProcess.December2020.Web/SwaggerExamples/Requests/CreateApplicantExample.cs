using Hahn.ApplicatonProcess.December2020.Domain;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.SwaggerExamples.Requests
{
    public class CreateApplicantExample : IExamplesProvider<ApplicantDTO>
    {
        public ApplicantDTO GetExamples()
        {
            return new ApplicantDTO
            {
                Name = "Toyeeb",
                FamilyName = "Adesokan",
                Address = "Lagos, Nigeria",
                Age = 25,
                EmailAddress = "adesokantayyib@gmail.com",
                CountryOfOrigin = "Nigeria",
                Hired = true
            };
        }
    }
}
