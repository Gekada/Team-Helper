using AutoMapper;
using TeamHelper.Application.Athletes.Commands;
using TeamHelper.Application.Coaches.Commands;
using TeamHelper.Application.Common.Mapping;

namespace TeamHelper.WebApi.Models
{
    public class UpdateAthleteCommandDTO : IMapWith<UpdateAthleteCommand>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid OrganizationId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAthleteCommandDTO, UpdateAthleteCommand>();
        }
    }
}
