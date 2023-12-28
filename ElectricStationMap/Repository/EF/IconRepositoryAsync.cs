using ElectricStationMap.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace ElectricStationMap.Repository.EF
{
    public class IconRepositoryAsync : GenericRepositoryAsync<Icon>, IIconRepositoryAsync
    {
        private DbSet<Icon> _icons;

        public IconRepositoryAsync(ElectricStationMapDBContext dbContext) : base(dbContext)
        {
            _icons = dbContext.Set<Icon>();
        }
    }
}
