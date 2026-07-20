using Gym.BusinessLogic.ViewModel.HealthRecords;
using Gym.BusinessLogic.ViewModel.Members;
using Gym.DataAccess.Entities;
using Gym.DataAccess.Enums;
using Gym.DataAccess.Repositries;

namespace Gym.BusinessLogic.Services
{
    public class MemberService(IUnitOfWork unitOfWork) : IMemberService
    {
        public async Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var members = await unitOfWork.Members.GetAllAsync(cancellationToken);

            return members.Select(m => new MemberIndexViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                PhotoUrl = m.Photo,
                JoinDate = DateOnly.FromDateTime(m.JoinDate),
                Gender = m.Gender.ToString(),
            });
        }

        public async Task<bool> CreateAsync(CreateMemberViewModel model, CancellationToken cancellationToken = default)
        {
            var email = model.Email.Trim().ToLower();
            var phone = model.Phone.Trim().ToLower();
            var name = model.Name.Trim();

            if (await unitOfWork.Members.ExistsAsync(m => m.Email == email, cancellationToken))
                return false;

            if (!Enum.TryParse(model.Gender, true, out Gender gender))
                return false;

            if (!Enum.TryParse(model.HealthRecord.BloodType, true, out BloodType bloodType))
                return false;

            var member = new Member
            {
                Name = name,
                Email = email,
                Phone = phone,
                DateOfBirth = model.DateOfBirth,
                Gender = gender,
                JoinDate = DateTime.UtcNow,

                Address = new DataAccess.Entities.ValueObject.Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },

                HealthRecord = new HealthRecord
                {
                    BloodType = bloodType,
                    Height = model.HealthRecord.Height,
                    Weight = model.HealthRecord.Weight,
                    Notes = model.HealthRecord.Note
                }
            };

            await unitOfWork.Members.AddAsync(member, cancellationToken);
            await unitOfWork.ComitAsync(cancellationToken);

            return true;
        }

        public async Task<MemberDetailsViewModel?> GetDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await unitOfWork.Members.GetByIdAsync(
                id,
                cancellationToken,
                m => m.MemberShips);

            if (member == null)
                return null;

            var latestMembership = member.MemberShips?
                .OrderByDescending(m => m.EndDate)
                .FirstOrDefault();

            return new MemberDetailsViewModel
            {
                Id = member.Id,
                Name = member.Name,
                PhotoUrl = member.Photo,
                Email = member.Email,
                PhoneNumber = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString("yyyy-MM-dd"),
                Address = member.Address != null
                    ? $"{member.Address.BuildingNumber}, {member.Address.Street}, {member.Address.City}"
                    : string.Empty,

                PlanName = latestMembership?.Plan?.Name ?? string.Empty,
                MembershipStartDate = latestMembership?.StartDate.ToString("yyyy-MM-dd") ?? string.Empty,
                MembershipEndDate = latestMembership?.EndDate.ToString("yyyy-MM-dd") ?? string.Empty
            };
        }

        public Task<string?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<HealthRecordDetailsViewModel?> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await unitOfWork.Members.GetByIdAsync(id, cancellationToken);

            if (member == null)
                throw new InvalidOperationException("Member not found.");

            return new HealthRecordDetailsViewModel
            {
                Height = member.HealthRecord.Height,
                Weight = member.HealthRecord.Weight,
                BloodType = member.HealthRecord.BloodType.ToString(),
                Note = member.HealthRecord.Notes
            };
        }

        public async Task<EditMemberViewModel?> GetForUpdateAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await unitOfWork.Members.GetByIdAsync(id, cancellationToken);

            if (member == null)
                return null;

            return new EditMemberViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                PhotoUrl = member.Photo,
                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City,
                Street = member.Address.Street
            };
        }

        public async Task<bool> UpdateAsync(EditMemberViewModel model, CancellationToken cancellationToken = default)
        {
            var member = await unitOfWork.Members.GetByIdAsync(model.Id, cancellationToken);

            if (member == null)
                return false;

            member.Name = model.Name.Trim();
            member.Email = model.Email.Trim().ToLower();
            member.Phone = model.Phone.Trim();
            member.Photo = model.PhotoUrl;

            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.City = model.City;
            member.Address.Street = model.Street;

            member.UpdatedAt = DateTime.UtcNow;

            unitOfWork.Members.Update(member);

            await unitOfWork.ComitAsync(cancellationToken);

            return true;
        }
    }
}