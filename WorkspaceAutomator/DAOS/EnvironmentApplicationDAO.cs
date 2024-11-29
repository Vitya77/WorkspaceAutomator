
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkspaceAutomator.Models;

namespace WorkspaceAutomator.DAOS
{
    public class EnvironmentApplicationsRepository
    {
        private readonly AppDbContext _context;

        public EnvironmentApplicationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EnvironmentApplication>> GetAllAsync()
        {
            return await _context.EnvironmentApplications
                .Include(ea => ea.Environment)
                .Include(ea => ea.Application)
                .ToListAsync();
        }

        public async Task<List<Application>> GetApplicationsForEnvironmentAsync(int environmentId)
        {
            return await _context.EnvironmentApplications
                .Where(ea => ea.EnvironmentId == environmentId)
                .Select(ea => ea.Application)
                .ToListAsync();
        }

        public async Task<List<Environment>> GetEnvironmentsForApplicationAsync(int applicationId)
        {
            return await _context.EnvironmentApplications
                .Where(ea => ea.ApplicationId == applicationId)
                .Select(ea => ea.Environment)
                .ToListAsync();
        }

        public async Task AddAsync(EnvironmentApplication environmentApplication)
        {
            await _context.EnvironmentApplications.AddAsync(environmentApplication);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int environmentId, int applicationId)
        {
            var environmentApplication = await _context.EnvironmentApplications
                .FirstOrDefaultAsync(ea => ea.EnvironmentId == environmentId && ea.ApplicationId == applicationId);

            if (environmentApplication != null)
            {
                _context.EnvironmentApplications.Remove(environmentApplication);
                await _context.SaveChangesAsync();
            }
        }
    }

}
