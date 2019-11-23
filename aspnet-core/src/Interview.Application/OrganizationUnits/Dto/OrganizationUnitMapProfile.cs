using Abp.Organizations;
using AutoMapper;

namespace Interview.OrganizationUnits.Dto
{
    public class OrganizationUnitMapProfile : Profile
    {
        public OrganizationUnitMapProfile()
        {
            CreateMap<CreateOrganizationUnitDto, OrganizationUnit>();

            CreateMap<OrganizationUnitDto, OrganizationUnit>();

            CreateMap<OrganizationUnit, OrganizationUnitDto>();

        }
    }
}
