using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkspaceAutomator.Models;

namespace WorkspaceAutomator.DAOS
{

    public class ApplicationsDAO
    {
        private readonly AppDbContext _context;

        public ApplicationsDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Application>> GetAllAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.EnvironmentApplications)
                .ThenInclude(ea => ea.Environment)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Application application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }
        }
    }

}
