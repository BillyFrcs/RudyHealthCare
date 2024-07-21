using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RudyHealthCare.Data;
using RudyHealthCare.Models.Patients;

namespace RudyHealthCare.Repositories.Patients
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Patients.CountAsync();
        }

        public async Task<IEnumerable<PatientsModel>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task AddAsync(PatientsModel patients)
        {
            _context.Patients.Add(patients);

            await _context.SaveChangesAsync();
        }
    }
}