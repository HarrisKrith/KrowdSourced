using Crowdfunding.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crowdfunding.Data
{
    public class CrowdfundingDbContext : IdentityDbContext
    {

        public CrowdfundingDbContext(DbContextOptions<CrowdfundingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Donation>()
                .ToTable("Donations");


            modelBuilder
                .Entity<Project>()
                .ToTable("Project");

            modelBuilder
                .Entity<UserSponsor>()
                .ToTable("UserSponsor");
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
