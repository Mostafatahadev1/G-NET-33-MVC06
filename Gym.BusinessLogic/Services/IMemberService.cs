using Gym.BusinessLogic.ViewModel.Members;
using Gym.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.BusinessLogic.ViewModel.Members;
<<<<<<< Updated upstream
using Gym.BusinessLogic.ViewModel.HealthRecords;
=======
using MySqlX.XDevAPI.Common;
>>>>>>> Stashed changes

namespace Gym.BusinessLogic.Services

    //view models => presentation Layer
    // view models => BLL (OK) => Presentation Layer 
    // 1 - Vw in BLL(problem reused ) 
    // 2- Vw in pressentation Layer (Best Practice )
    // BLL => Dtos 
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken cancellationToken = default);

<<<<<<< Updated upstream
        public Task <bool> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default);


        Task<MemberDetailsViewModel?> GetDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task<string?> GetByIdAsync(int id, CancellationToken cancellationToken);


        Task<HealthRecordDetailsViewModel>GetHealthRecordAsync(int id, CancellationToken cancellationToken = default);
        Task<string?> GetForUpdateAsync(int id, CancellationToken cancellationToken);
=======
        public Task<bool> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default);

        Task<EditMemberViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(EditMemberViewModel editMemberViewModel ,CancellationToken cancellationToken = default);



>>>>>>> Stashed changes
    }


} 