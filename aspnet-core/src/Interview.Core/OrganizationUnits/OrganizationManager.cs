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
            var code = await GetNextCodeAsync(organizationUnit.ParentId);
            if(organizationUnit.ParentId != null)
            {
                code = await GetCodeAsync(organizationUnit.ParentId.Value) + "." + code;
            }
            organizationUnit.Code = code;
            await _organizationUnitRepository.InsertAsync(organizationUnit);
        }

        public async Task<string> GetNextCodeAsync(long? parentId)
        {
            var children = await _organizationUnitRepository.GetAllListAsync(x => x.ParentId == parentId);
            var lastChild = children.OrderBy(x => x.Code).LastOrDefault();
            if(lastChild == null)
            {
                return GenerateNextCodeAsync("0");
            }
            return GenerateNextCodeAsync(lastChild.Code);
        }

        public async Task<string> GetCodeAsync(long id)
        {
            var ou = await _organizationUnitRepository.FirstOrDefaultAsync(id);
            if(ou == null)
            {
                throw new UserFriendlyException(L("OrganizationUnitNotFound", id));
            }
            return ou.Code;
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

        }
    }
}
