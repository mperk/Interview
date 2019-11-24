using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.UI;
using Interview.OrganizationUnits.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task UpdateDisplayName(UpdateOrganizationUnitDto input)
        {
            await _organizationManager.UpdateDisplayNameAsync(input.Id, input.DisplayName);
        }

        public async Task Delete(long id)
        {
            await _organizationManager.DeleteOrganizationUnitAsync(id);
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetList()
        {
            var organizationUnits = await _repository
                .GetAll()
                .OrderBy(x => x.Code)
                .ToListAsync();

            return new ListResultDto<OrganizationUnitDto>(organizationUnits.MapTo<List<OrganizationUnitDto>>());
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetTreeList()
        {
            var organizationUnits = await _repository
                .GetAll()
                .Include(x => x.Children)
                .OrderBy(x => x.Code)
                .ToListAsync();

            return new ListResultDto<OrganizationUnitDto>(organizationUnits.MapTo<List<OrganizationUnitDto>>());
        }

        public async Task<OrganizationUnitDto> Get(EntityDto<long> input)
        {
            var organizationUnit = await _repository
                .GetAsync(input.Id);

            if (organizationUnit == null)
            {
                throw new UserFriendlyException("Could not found the organization unit, maybe it's deleted.");
            }

            return organizationUnit.MapTo<OrganizationUnitDto>();
        }

    }
}
