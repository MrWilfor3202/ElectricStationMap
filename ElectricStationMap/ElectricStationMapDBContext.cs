using ElectricStationMap.Models.EF;
using ElectricStationMap.Models.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectricStationMap
{
    public class ElectricStationMapDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ElectricStationMapDBContext(DbContextOptions<ElectricStationMapDBContext> options) : base(options) { }

        public DbSet<RequestInfo> Requests { get; set; }

        public DbSet<RequirementInfo> Requirements { get; set; }

        public DbSet<Icon> Icons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
