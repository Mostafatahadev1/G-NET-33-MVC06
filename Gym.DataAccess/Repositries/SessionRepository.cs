using Gym.DataAccess.Entities;
using Gym.Presentation.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositries
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
    
    
        public SessionRepository(GymDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
