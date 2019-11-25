using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Interview.Authorization.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview.OrganizationUnits.Users
{
    public interface IUserManagerInOrganizationUnit : IDomainService
    {
        Task<UserOrganizationUnit> AddUserToOrganizationUnitAsync(long userId, long organizationUnitId);

        Task<UserOrganizationUnit> FindUserOrganizationUnitAsync(int? tenantId, long userId, long organizationUnitId);

        Task DeleteUserOrganizationUnitAsync(long id);

        Task<List<UserOrganizationUnit>> FindUserOrganizationUnitsAsync(long organizationUnitId);

        Task<IEnumerable<User>> GetUsersInOrganizationUnitAsync(long organizationUnitId);

        Task<List<User>> GetUsersInOrganizationUnitWithPageAsync(PagedResultRequestDto paged, long organizationUnitId);

        Task<int> GetUsersInOrganizationUnitCountAsync(long organizationUnitId);

        Task<IEnumerable<User>> GetUsersNotInOrganizationUnitAsync(long organizationUnitId);

        Task<List<User>> GetUsersNotInOrganizationUnitWithPageAsync(PagedResultRequestDto paged, long organizationUnitId);

        Task<int> GetUsersNotInOrganizationUnitCountAsync(long organizationUnitId);
    }
}
