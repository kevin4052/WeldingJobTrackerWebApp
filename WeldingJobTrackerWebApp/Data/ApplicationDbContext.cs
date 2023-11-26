using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeldingJobTrackerWebApp.Models;

namespace WeldingJobTrackerWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamRole> TeamRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<User>()
                .HasMany(u => u.Companies)
                .WithMany(c => c.Employees)
                .UsingEntity("Employees");

            builder.Entity<Team>()
                .HasMany(t => t.Projects)
                .WithOne(p => p.Team);

            builder.Entity<Team>()
                .HasMany(t => t.TeamMembers)
                .WithOne(tm => tm.Team);

            base.OnModelCreating(builder);
        }
    }
}
