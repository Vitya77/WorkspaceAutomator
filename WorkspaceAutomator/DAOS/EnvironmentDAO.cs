using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkspaceAutomator.Models;

namespace WorkspaceAutomator.DAOS
{
    public class EnvironmentDAO
    {
        private readonly AppDbContext _context;

        public EnvironmentDAO(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Environment>> GetAllAsync()
        {
            return await _context.Environments.ToListAsync();
        }

        public async Task<Environment> GetByIdAsync(int id)
        {
            return await _context.Environments
                .Include(e => e.EnvironmentApplications)
                .ThenInclude(ea => ea.Application)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Environment environment)
        {
            _context.Environments.Add(environment);
            await _context.SaveChangesAsync();
        }
    }
}
