using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Data.Repository.Interfaces;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Implementations
{
    public class ApplicantService : IApplicantService
    {
        private readonly IAppRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicantService> _logger;
        public ApplicantService(IMapper mapper, IAppRepository repository, ILogger<ApplicantService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<(ApplicantDTO, string)> CreateNewApplicant(ApplicantDTO applicantDTO)
        {
            try
            {
                var applicant = _mapper.Map<Applicant>(applicantDTO);
                applicant.ID = Guid.NewGuid();
                applicant.CreationDate = DateTime.Now;
                applicant.LastModifiedDate = DateTime.Now;

                await _repository.Add(applicant);

                if(await _repository.SaveAllChangesAsync())
                {
                    return (_mapper.Map<ApplicantDTO>(applicant), String.Empty);
                }

                return (null, "Failed to Save Changes");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (null, "An unexpected error occured");
            }
        }

        public async Task<(string, string)> DeleteApplicant(string ID)
        {
            try
            {
                var applicant = await _repository.GetApplicant(ID);

                if (applicant != null)
                {
                    _repository.Delete(applicant);

                    if(await _repository.SaveAllChangesAsync())
                    {
                        return (ID, null);
                    }

                }
                return (null, $"Applicant with ID {ID} not found");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (null, "An unexpected error occured");
            }

        }

        public async Task<(ApplicantDTO, string)> GetApplicant(string ID)
        {
            try
            {
                var applicantToReturn =  _mapper.Map<ApplicantDTO>(await _repository.GetApplicant(ID));

                if(applicantToReturn == null)
                {
                    return (null, $"Applicant with ID {ID} not found");
                }

                return (applicantToReturn, null);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (null, "An unexpected error occured");
            }
        }

        public async Task<(ApplicantDTO, string)> UpdateApplicant(string ID, ApplicantDTO applicantDTO)
        {
            try
            {
                var applicant = await _repository.GetApplicant(ID);

                if (applicant == null)
                {
                    return (null, $"Applicant with ID {ID} not found");
                }

                var applicantToUpdate = _mapper.Map<ApplicantDTO, Applicant>(applicantDTO, applicant);
                _repository.Update(applicantToUpdate);

                if (await _repository.SaveAllChangesAsync())
                {
                    var applicantToReturn = _mapper.Map<ApplicantDTO>(applicantToUpdate);
                    return (applicantToReturn, null);
                }
                return (null, "Failed to Save Changes");
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (null, "An unexpected error occured");
            }
        }
    }
}
