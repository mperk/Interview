using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.OrganizationUnits
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

        public async Task CreateAsync(OrganizationUnit organizationUnit)
        {
            organizationUnit.Code = await GetNextCodeAsync(organizationUnit.ParentId);
            await _organizationUnitRepository.InsertAsync(organizationUnit);
        }

        public async Task<string> GetNextCodeAsync(long? parentId)
        {
            var children = await _organizationUnitRepository.GetAllListAsync(x => x.ParentId == parentId);
            string maxCode = children.OrderBy(x => x.Code).LastOrDefault().Code;
            return GenerateNextCodeAsync(maxCode);
        }

        public string GenerateNextCodeAsync(string code)
        {
            var lastPart = GetLastPart(code);
            return code.Replace(lastPart, CreateNextLastPart(lastPart));
        }

        public string CreateNextLastPart(string lastPart)
        {
            var nextNumber = Convert.ToInt32(lastPart) + 1;
            return nextNumber.ToString().PadLeft(OrganizationUnit.CodeUnitLength, '0');
        }

        public string GetLastPart(string code)
        {
            var parts = code.Split('.');
            return parts[parts.Length - 1];
        }

        public async Task MoveAsync(long id, long? parentId)
        {

        }
    }
}
