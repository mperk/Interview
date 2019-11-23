using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Interview.OrganizationUnits.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interview.OrganizationUnits
{
    public class OrganizationUnitAppService : InterviewAppServiceBase, IOrganizationUnitAppService
    {
        private readonly IRepository<OrganizationUnit, long> _repository;
        private readonly IOrganizationManager _organizationManager;

        public OrganizationUnitAppService(IRepository<OrganizationUnit, long> repository,
            IOrganizationManager organizationManager)
        {
            _repository = repository;
            _organizationManager = organizationManager;
        }

        public async Task Create(CreateOrganizationUnitDto input)
        {
            var organizationUnit = ObjectMapper.Map<OrganizationUnit>(input);
            await _organizationManager.CreateAsync(organizationUnit);
        }

    }
}
