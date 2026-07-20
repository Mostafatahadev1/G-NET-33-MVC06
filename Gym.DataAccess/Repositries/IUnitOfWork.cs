using Gym.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositries
{
    public interface IUnitOfWork : IAsyncDisposable

    {
        IMemberRepository Members { get; }

        IPlanRepository Plans { get; }

        ITrainerRepository Trains { get; }

        ISeesionRepository Sessions { get; }

        IBookingRepository Bookings { get; }


        IRepository<Category>Catagories { get; }

        IRepository<HealthRecord> HealthRecords { get; } 


        Task<int> ComitAsync(CancellationToken cancellationToken);


        Task BeginTransactinAsync(CancellationToken cancellationToken);


        Task ComitTransactionAsAsync(CancellationToken cancellationToken);


        Task RollBackTransactionAsync(CancellationToken cancellationToken);







    }
}
