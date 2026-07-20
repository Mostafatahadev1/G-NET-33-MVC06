using Gym.DataAccess.Entities;
using Gym.Presentation.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Gym.DataAccess.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;
        private IDbContextTransaction? _dbTransaction;

        private IMemberRepository? _members;
        private IPlanRepository? _plans;
        private ITrainerRepository? _trainers;
        private ISeesionRepository? _sessions;
        private IBookingRepository? _bookings;
        private IRepository<Category>? _categories;
        private IRepository<HealthRecord>? _healthRecords;

        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMemberRepository Members
            => _members ??= new MemberRepository(_dbContext);

        public IPlanRepository Plans
            => _plans ??= new PlanRepository(_dbContext);

        public ITrainerRepository Trains
            => _trainers ??= new TrainerRepository(_dbContext);



        public IRepository<Category> Catagories
            => _categories ??= new Repository<Category>(_dbContext);

        public IRepository<HealthRecord> HealthRecords
            => _healthRecords ??= new Repository<HealthRecord>(_dbContext);

        public ISeesionRepository Sessions => throw new NotImplementedException();

        public IBookingRepository Bookings => throw new NotImplementedException();

        public async Task<int> ComitAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactinAsync(CancellationToken cancellationToken = default)
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task ComitTransactionAsAsync(CancellationToken cancellationToken = default)
        {
            if (_dbTransaction != null)
                await _dbTransaction.CommitAsync(cancellationToken);
        }

        public async Task RollBackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_dbTransaction != null)
                await _dbTransaction.RollbackAsync(cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            if (_dbTransaction != null)
                await _dbTransaction.DisposeAsync();

            await _dbContext.DisposeAsync();
        }
    }
}