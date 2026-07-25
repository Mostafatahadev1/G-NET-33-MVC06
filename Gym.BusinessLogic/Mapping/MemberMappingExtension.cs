using Gym.BusinessLogic.ViewModel.Members;
using Gym.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Mapping
{
    public static class MemberMappingExtension
    {
        public static EditMemberViewModel ToEditMemberViewModel(this Member member)
        {
            return new EditMemberViewModel
            {
                Id = member.Id,
                Name = member.Name,
                PhotoUrl = member.Photo,
                Email = member.Email,
                Phone = member.Phone,

                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City,
                Street = member.Address.Street
            };
        }

        public static MemberIndexViewModel ToIndexViewModel(this Member member)
        {
            return new MemberIndexViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                PhotoUrl = member.Photo,
                JoinDate = DateOnly.FromDateTime(member.JoinDate),
                Gender = member.Gender.ToString()
            };
        }

        public static MemberDetailsViewModel ToDetailsViewModel(this Member member)
        {
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

                Address = member.Address == null
                    ? string.Empty
                    : $"{member.Address.BuildingNumber}, {member.Address.Street}, {member.Address.City}",

                PlanName = latestMembership?.Plan?.Name ?? string.Empty,
                MembershipStartDate = latestMembership?.StartDate.ToString("yyyy-MM-dd") ?? string.Empty,
                MembershipEndDate = latestMembership?.EndDate.ToString("yyyy-MM-dd") ?? string.Empty
            };
        }
    }
}
