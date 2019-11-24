using Abp.Application.Services.Dto;
using Abp.Organizations;
using AutoMapper;
using Interview.Authorization.Users;
using Interview.Extensions;
using Interview.Users.Dto;
using System.Collections.Generic;

namespace Interview.OrganizationUnits.Dto
{
    public class OrganizationUnitMapProfile : Profile
    {
        public OrganizationUnitMapProfile()
        {
            CreateMap<CreateOrganizationUnitDto, OrganizationUnit>();

            CreateMap<OrganizationUnitDto, OrganizationUnit>();

            CreateMap<OrganizationUnit, OrganizationUnitDto>().ForMember(x => x.Children, x => x.MapFrom(y => y.Children));

            CreateMap<TreeItem<OrganizationUnit>, TreeItem<OrganizationUnitDto>>().ForMember(x => x.Children, x=> x.MapFrom(y => y.Children));
            
        }
    }
}
