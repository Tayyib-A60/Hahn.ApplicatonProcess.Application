using Hahn.ApplicatonProcess.December2020.Data.Helpers;
using Hahn.ApplicatonProcess.December2020.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository.Interfaces
{
    public interface IAppRepository: IAutoDependencyService
    {
        Task Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T: class;
        Task<Applicant> GetApplicant(string id);
        Task<bool> SaveAllChangesAsync();
    }
}
