
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkspaceAutomator.Models;

namespace WorkspaceAutomator.DAOS
{
    public class AppDbContext : DbContext
    {
        public DbSet<Environment> Environments { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<EnvironmentApplication> EnvironmentApplications { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnvironmentApplication>()
                .HasKey(ea => new { ea.EnvironmentId, ea.ApplicationId });

            modelBuilder.Entity<EnvironmentApplication>()
                .HasOne(ea => ea.Environment)
                .WithMany(e => e.EnvironmentApplications)
                .HasForeignKey(ea => ea.EnvironmentId);

            modelBuilder.Entity<EnvironmentApplication>()
                .HasOne(ea => ea.Application)
                .WithMany(a => a.EnvironmentApplications)
                .HasForeignKey(ea => ea.ApplicationId);
        }
    }

}
