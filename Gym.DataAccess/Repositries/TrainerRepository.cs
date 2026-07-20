using Gym.DataAccess.Entities;
using Gym.Presentation.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositries
{
    public class TrainerRepository : Repository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(GymDbContext dbContext) : base(dbContext)
        {
        }
    }
}
