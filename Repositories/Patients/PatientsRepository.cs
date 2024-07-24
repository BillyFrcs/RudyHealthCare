using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;

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

        public async Task<int> GetTotalCountQueueStatusAsync(string queueStatus)
        {
            return await _context.Patients.CountAsync(patients => patients.QueueStatus == queueStatus);
        }

        public async Task<IEnumerable<PatientsModel>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<IEnumerable<PatientsModel>> GetPatientsAsync(int pageNumber, int pageSize)
        {
            return await _context.Patients.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<PatientsModel?> GetByIdAsync(string patientId)
        {
            return await _context.Patients.FindAsync(patientId);
        }

        public async Task<IEnumerable<PatientsModel>> GetByQueueStatusAsync(string queueStatus, int pageNumber, int pageSize)
        {
            return await _context.Patients.Where(patients => patients.QueueStatus == queueStatus).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task AddAsync(PatientsModel patients)
        {
            _context.Patients.Add(patients);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PatientsModel patients)
        {
            _context.Entry(patients).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PatientsModel patients)
        {
            _context.Patients.Remove(patients);

            await _context.SaveChangesAsync();
        }
    }
}