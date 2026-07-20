using Gym.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositries
{
    public interface IMemberRepository : IRepository<Member>
    {
<<<<<<< Updated upstream
=======
        Task<bool> IsEmailTakenAsync(string normalizedEmail, string normalizedPhone);
>>>>>>> Stashed changes
        Task<bool> IsEmailTakenAsync(string normalizedEmail, string normalizedPhone);
    }
}
