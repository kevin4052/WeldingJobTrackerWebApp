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
                .HasOne(u => u.Company)
                .WithMany(c => c.Employees);

            builder.Entity<Team>()
                .HasMany(t => t.Projects)
                .WithOne(p => p.Team);

            builder.Entity<Team>()
                .HasMany(t => t.TeamMembers)
                .WithOne(tm => tm.Team);

            builder.Entity<ProjectStatus>().HasData(
                new { Id = 1, Code = "Draft", Name = "Draft" },
                new { Id = 2, Code = "Pending", Name = "Pending" },
                new { Id = 3, Code = "Active", Name = "Active" },
                new { Id = 4, Code = "OnHold", Name = "OnHold" },
                new { Id = 5, Code = "InActive", Name = "InActive" },
                new { Id = 6, Code = "Completed", Name = "Completed" }
            );

            builder.Entity<TeamRole>().HasData(
                new { Id = 1, Code = "ProjectManager", Name = "Project Manager" },
                new { Id = 2, Code = "Supervisor", Name = "Supervisor" },
                new { Id = 3, Code = "Engineer", Name = "Engineer" },
                new { Id = 4, Code = "Drafter", Name = "Drafter" },
                new { Id = 5, Code = "Labor", Name = "Labor" }
            );

            base.OnModelCreating(builder);
        }
    }
}
