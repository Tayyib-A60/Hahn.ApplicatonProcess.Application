using Hahn.ApplicatonProcess.December2020.Data.Models;
using Hahn.ApplicatonProcess.December2020.Data.Persistence;
using Hahn.ApplicatonProcess.December2020.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository.Implementations
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _appDbContext;
        public AppRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _appDbContext.AddAsync(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _appDbContext.Remove(entity);
        }

        public async Task<Applicant> GetApplicant(string id)
        {
            return await _appDbContext.Applicants.FirstOrDefaultAsync(app => app.ID.ToString() == id);  
        }

        public void Update<T>(T entity) where T : class
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            if (await _appDbContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
