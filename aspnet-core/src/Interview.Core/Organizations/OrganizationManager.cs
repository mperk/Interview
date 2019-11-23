using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Organizations
{
    public class OrganizationManager : DomainService, IOrganizationManager
    {
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;

        public OrganizationManager(
            IRepository<OrganizationUnit, long> organizationUnitRepository
            )
        {
            _organizationUnitRepository = organizationUnitRepository;
        }

        public async Task MoveAsync(long id, long? parentId)
        {

        }
    }
}
