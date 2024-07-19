using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RudyHealthCare.Models.Patients;

namespace RudyHealthCare.Repositories.Patients
{
    public interface IPatientsRepository
    {
        Task<IEnumerable<RegistrationFormsModel>> GetAllAsync();
        Task AddAsync(RegistrationFormsModel patient);
    }
}