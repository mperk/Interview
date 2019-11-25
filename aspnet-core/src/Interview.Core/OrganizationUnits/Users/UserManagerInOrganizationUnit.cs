using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Interview.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.OrganizationUnits.Users
{
    public class UserManagerInOrganizationUnit : DomainService, IUserManagerInOrganizationUnit
    {
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<User, long> _userRepository;

        public UserManagerInOrganizationUnit(IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
             IRepository<User, long> userRepository)
        {
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _userRepository = userRepository;
        }

        public async Task<UserOrganizationUnit> AddUserToOrganizationUnitAsync(long userId, long organizationUnitId)
        {
            return await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit() { OrganizationUnitId = organizationUnitId, UserId = userId });
        }

        public async Task<UserOrganizationUnit> FindUserOrganizationUnitAsync(int? tenantId, long userId, long organizationUnitId)
        {
            return await _userOrganizationUnitRepository.FirstOrDefaultAsync(x => 
                        x.TenantId == tenantId &&
                        x.UserId == userId &&
                        x.OrganizationUnitId == organizationUnitId);
        }

        public async Task DeleteUserFromOrganizationUnitAsync(long id)
        {
            await _userOrganizationUnitRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersInOrganizationUnitAsync(long organizationUnitId)
        {
            var users = (await _userOrganizationUnitRepository.GetAllListAsync())
                .Where(x => x.OrganizationUnitId == organizationUnitId)
                .Join(await _userRepository.GetAllListAsync(), ou => ou.UserId, user => user.Id, (ou, user) => user);
            return users;
        }

        public async Task<List<User>> GetUsersInOrganizationUnitWithPageAsync(PagedResultRequestDto paged, long organizationUnitId)
        {
            var users = (await GetUsersInOrganizationUnitAsync(organizationUnitId))
                        .Skip(paged.SkipCount)
                        .Take(paged.MaxResultCount);
            return users.ToList();
        }

        public async Task<int> GetUsersInOrganizationUnitCountAsync(long organizationUnitId)
        {
            return (await GetUsersInOrganizationUnitAsync(organizationUnitId)).Count();
        }

        public async Task<IEnumerable<User>> GetUsersNotInOrganizationUnitAsync(long organizationUnitId)
        {
            var users = (await _userRepository.GetAllListAsync())
                        .Except(
                                (await _userOrganizationUnitRepository.GetAllListAsync())
                                .Where(x => x.OrganizationUnitId == organizationUnitId)
                                .Join(await _userRepository.GetAllListAsync(), ou => ou.UserId, u => u.Id, (ou, user) => user)
                                );
            return users;
        }

        public async Task<List<User>> GetUsersNotInOrganizationUnitWithPageAsync(PagedResultRequestDto paged, long organizationUnitId)
        {
            var users = (await GetUsersNotInOrganizationUnitAsync(organizationUnitId))
                        .Skip(paged.SkipCount)
                        .Take(paged.MaxResultCount);
            return users.ToList();
        }

        public async Task<int> GetUsersNotInOrganizationUnitCountAsync(long organizationUnitId)
        {
            return (await GetUsersNotInOrganizationUnitAsync(organizationUnitId)).Count();
        }

    }
}
