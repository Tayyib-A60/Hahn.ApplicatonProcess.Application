using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicantDTO, Applicant>();
            CreateMap<Applicant, ApplicantDTO>();
        }
    }
}
