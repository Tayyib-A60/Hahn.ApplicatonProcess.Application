using Hahn.ApplicatonProcess.December2020.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces
{
    public interface IApplicantService: IAutoDependencyService
    {
        Task<(ApplicantDTO, string)> CreateNewApplicant(ApplicantDTO applicantDTO);
        Task<(string, string)> DeleteApplicant(string ID);
        Task<(ApplicantDTO, string)> UpdateApplicant(string ID, ApplicantDTO applicantDTO);
        Task<(ApplicantDTO, string)> GetApplicant(string ID);
    }
}
