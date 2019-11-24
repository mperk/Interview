using Abp.Domain.Services;
using Abp.Organizations;
using System.Threading.Tasks;

namespace Interview.OrganizationUnits
{
    public interface IOrganizationManager : IDomainService
    {
        Task CreateAsync(OrganizationUnit organizationUnit);

        Task<OrganizationUnit> UpdateDisplayNameAsync(long id, string displayName);

        Task DeleteOrganizationUnitAsync(long id);

        Task MoveAsync(long id, long? parentId);
    }
}
