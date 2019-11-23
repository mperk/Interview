using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Interview.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Organizations
{
    public class OrganizationAppService : AsyncCrudAppService<OrganizationUnit, OrganizationUnitDto, long, PagedOrganizationUnitResultRequestDto,
                                          CreateOrganizationUnitDto, OrganizationUnitDto>, IOrganizationAppService
    {
        public OrganizationAppService(IRepository<OrganizationUnit, long> repository)
            : base(repository)
        {

        }
    }
}
