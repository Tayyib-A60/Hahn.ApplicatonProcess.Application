using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Helpers
{
    public class ApplicantValidator: AbstractValidator<ApplicantDTO>
    {
        private readonly IUtilities _utilities;
        public ApplicantValidator( IUtilities utilities)
        {
            _utilities = utilities;

            RuleFor(applicant => applicant.Name).MinimumLength(5);
            RuleFor(applicant => applicant.FamilyName).MinimumLength(5);
            RuleFor(applicant => applicant.Age).InclusiveBetween(20, 60);
            RuleFor(applicant => applicant.Address).MinimumLength(10);
            RuleFor(applicant => applicant.EmailAddress).EmailAddress();
            RuleFor(applicant => applicant.CountryOfOrigin).MustAsync(async(countryOfOrigin, cancellation) => await BeAValidCountry(countryOfOrigin)).WithMessage("Country is not valid");
        }

        private async Task<bool> BeAValidCountry(string countryOfOrigin)
        {
            var requestUri = $"{countryOfOrigin}?fullText=true";
            var response = await _utilities.MakeHttpRequest(null, "https://restcountries.eu/rest/v2/name/", requestUri, HttpMethod.Get);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
