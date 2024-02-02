namespace ElectricStationMap.Repository.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElectricStationMapDBContext _dbContext;
        private bool _disposed;

        public UnitOfWork(ElectricStationMapDBContext dbContext) 
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
