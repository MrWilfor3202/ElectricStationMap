namespace ElectricStationMap.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
    }
}
