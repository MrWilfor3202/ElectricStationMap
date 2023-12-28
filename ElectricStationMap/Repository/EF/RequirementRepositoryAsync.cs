using ElectricStationMap.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ElectricStationMap.Repository.EF
{
    public class RequirementRepositoryAsync : GenericRepositoryAsync<RequirementInfo>, IRequirementRepositoryAsync
    {
        private DbSet<RequirementInfo> _requirements;

        public RequirementRepositoryAsync(ElectricStationMapDBContext dbContext) : base(dbContext)
        {
            _requirements = dbContext.Set<RequirementInfo>();
        }

        public Task<bool> HasRequirement(int id)
        {
            throw new NotImplementedException();
        }
    }
}
