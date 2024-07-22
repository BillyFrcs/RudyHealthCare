using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RudyHealthCare.Models.Patients;

namespace RudyHealthCare.Repositories.Patients
{
    public interface IPatientsRepository
    {
        public Task<int> GetTotalCountAsync();
        public Task<int> GetTotalCountQueueStatusAsync(string queueStatus);
        public Task<IEnumerable<PatientsModel>> GetAllAsync();
        public Task AddAsync(PatientsModel patients);
    }
}