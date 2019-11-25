using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Organizations;
using Abp.UI;
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
            var lastChild = children.OrderBy(x => x.Code).LastOrDefault();
            if (lastChild == null)
            {
                if (parentId != null)
                {
                    var parentCode = await GetCodeOrDefaultAsync(parentId.Value);
                    return parentCode + "." + GenerateNextCodeAsync("0");
                }
                else
                {
                    return GenerateNextCodeAsync("0");
                }
            }
            return GenerateNextCodeAsync(lastChild.Code);
        }

        public async Task<string> GetCodeOrDefaultAsync(long id)
        {
            var ou = await _organizationUnitRepository.FirstOrDefaultAsync(id);
            return ou?.Code;
        }

        public string GenerateNextCodeAsync(string code)
        {
            var lastPart = GetLastPart(code);
            var parts = code.Split(".");
            parts[parts.Length - 1] = CreateNextLastPart(lastPart);
            return String.Join(".", parts);
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

        public async Task<OrganizationUnit> UpdateDisplayNameAsync(long id, string displayName)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);
            organizationUnit.DisplayName = displayName;
            return await _organizationUnitRepository.UpdateAsync(organizationUnit);
        }

        public async Task DeleteOrganizationUnitAsync(long id)
        {
            await _organizationUnitRepository.DeleteAsync(id);
        }

        public async Task MoveAsync(long id, long? parentId)
        {
            var organizationUnit = await _organizationUnitRepository.FirstOrDefaultAsync(id);
            var children = await FindChildrenAsync(id);
            organizationUnit.ParentId = parentId;
            organizationUnit.Code = await GetNextCodeAsync(organizationUnit.ParentId);

            foreach (var child in children)
            {
                var parts = child.Code.Split(".");
                child.Code = organizationUnit.Code + "." + parts[parts.Length - 1];
            }

            await _organizationUnitRepository.UpdateAsync(organizationUnit);
        }

        public async Task<List<OrganizationUnit>> FindChildrenAsync(long id)
        {
            var parentCode = await GetCodeOrDefaultAsync(id);
            return await _organizationUnitRepository.GetAllListAsync(x => x.Code.StartsWith(parentCode) && x.Id != id);
        }
    }
}
