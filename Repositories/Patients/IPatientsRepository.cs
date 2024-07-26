using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Mvc.Core;

using RudyHealthCare.Models.Patients;

namespace RudyHealthCare.Repositories.Patients
{
    public interface IPatientsRepository
    {
        public Task<int> GetTotalCountAsync();
        public Task<int> GetTotalCountQueueStatusAsync(string queueStatus);
        public Task<IEnumerable<PatientsModel>> GetAllAsync(int pageNumber, int pageSize);
        public Task<IEnumerable<PatientsModel>> GetAllQueueStatusAsync(string searchTerm, int pageNumber, int pageSize);
        public Task<IEnumerable<PatientsModel>> GetPatientsAsync(int pageNumber, int pageSize);
        public Task<PatientsModel?> GetByIdAsync(string patientId);
        public Task<IEnumerable<PatientsModel>> GetByQueueStatusAsync(string queueStatus, int pageNumber, int pageSize);
        public Task AddAsync(PatientsModel patients);
        public Task UpdateAsync(PatientsModel patients);
        public Task DeleteAsync(PatientsModel patients);
    }
}