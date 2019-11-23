using Abp.Application.Services;
using Abp.Organizations;
using Interview.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Organizations
{
    public interface IOrganizationAppService : IAsyncCrudAppService<OrganizationUnitDto, long, PagedOrganizationUnitResultRequestDto, CreateOrganizationUnitDto, OrganizationUnitDto>
    {

    }
}
