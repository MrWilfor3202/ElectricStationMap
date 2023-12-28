using ElectricStationMap.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ElectricStationMap.Repository.EF
{
    public class RequestInfoRepositoryAsync : GenericRepositoryAsync<RequestInfo>, IRequestRepositoryAsync
    {
        private readonly DbSet<RequestInfo> _requests;

        public RequestInfoRepositoryAsync(ElectricStationMapDBContext dbContext) : base(dbContext)
        {
            _requests = dbContext.Set<RequestInfo>();
        }
    }
}
