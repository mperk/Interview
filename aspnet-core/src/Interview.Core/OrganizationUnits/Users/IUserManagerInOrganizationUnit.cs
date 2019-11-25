using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using Interview.Authorization.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview.OrganizationUnits.Users
{
    public interface IUserManagerInOrganizationUnit : IDomainService
    {
        Task<List<User>> GetUsersInOrganizationUnitAsync(PagedResultRequestDto paged, long organizationUnitId);

        Task<List<User>> GetUsersNotInOrganizationUnitAsync(PagedResultRequestDto paged, long organizationUnitId);
    }
}
