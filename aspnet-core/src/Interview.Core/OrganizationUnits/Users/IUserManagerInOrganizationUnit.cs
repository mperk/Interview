using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Interview.Authorization.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.OrganizationUnits.Users
{
    public interface IUserManagerInOrganizationUnit : IDomainService
    {
        Task<UserOrganizationUnit> AddUserToOrganizationUnitAsync(long userId, long organizationUnitId);

        Task<UserOrganizationUnit> FindUserOrganizationUnitAsync(int? tenantId, long userId, long organizationUnitId);

        Task DeleteUserOrganizationUnitAsync(long id);

        Task<List<UserOrganizationUnit>> FindUserOrganizationUnitsAsync(long organizationUnitId);

        IQueryable<User> GetUsersInOrganizationUnit(long organizationUnitId);

        List<User> GetUsersInOrganizationUnitWithPage(PagedResultRequestDto paged, long organizationUnitId);

        int GetUsersInOrganizationUnitCount(long organizationUnitId);

        IQueryable<User> GetUsersNotInOrganizationUnit(long organizationUnitId);

        List<User> GetUsersNotInOrganizationUnitWithPage(PagedResultRequestDto paged, long organizationUnitId);

        int GetUsersNotInOrganizationUnitCount(long organizationUnitId);
    }
}
