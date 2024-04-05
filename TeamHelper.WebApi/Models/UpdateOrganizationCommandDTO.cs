using AutoMapper;
using TeamHelper.Application.Common.Mapping;
using TeamHelper.Application.Organizations.Commands;

namespace TeamHelper.WebApi.Models
{
    public class UpdateOrganizationCommandDTO: IMapWith<UpdateOrganizationCommand>
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrganizationCommandDTO, UpdateOrganizationCommand>();
        }
    }
}
