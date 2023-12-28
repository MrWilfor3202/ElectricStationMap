using ElectricStationMap.Models.EF;
using ElectricStationMap.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ElectricStationMap
{
    public class ElectricStationMapDBContext : DbContext
    {
        public ElectricStationMapDBContext(DbContextOptions<ElectricStationMapDBContext> options) : base(options) { }

        public DbSet<RequestInfo> Requests { get; set; }

        public DbSet<RequirementInfo> Requirements { get; set; }

        public DbSet<Icon> Icons { get; set; }

    }
}
