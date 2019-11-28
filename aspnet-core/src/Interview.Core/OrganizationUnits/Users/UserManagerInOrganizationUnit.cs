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

        public async Task DeleteUserOrganizationUnitAsync(long id)
        {
            await _userOrganizationUnitRepository.DeleteAsync(id);
        }

        public async Task<List<UserOrganizationUnit>> FindUserOrganizationUnitsAsync(long organizationUnitId)
        {
            return await _userOrganizationUnitRepository.GetAllListAsync(x => x.OrganizationUnitId == organizationUnitId);
        }

        public IQueryable<User> GetUsersInOrganizationUnit(long organizationUnitId)
        {
            var users = _userOrganizationUnitRepository.GetAll()
                .Where(x => x.OrganizationUnitId == organizationUnitId)
                .Join(_userRepository.GetAll(), ou => ou.UserId, user => user.Id, (ou, user) => user);
            return users;
        }

        public List<User> GetUsersInOrganizationUnitWithPage(PagedResultRequestDto paged, long organizationUnitId)
        {
            var users = GetUsersInOrganizationUnit(organizationUnitId)
                        .Skip(paged.SkipCount)
                        .Take(paged.MaxResultCount);
            return users.ToList();
        }

        public int GetUsersInOrganizationUnitCount(long organizationUnitId)
        {
            return GetUsersInOrganizationUnit(organizationUnitId).Count();
        }

        public IQueryable<User> GetUsersNotInOrganizationUnit(long organizationUnitId)
        {
            var users = _userRepository.GetAll()
                        .Except(
                                (_userOrganizationUnitRepository.GetAll())
                                .Where(x => x.OrganizationUnitId == organizationUnitId)
                                .Join(_userRepository.GetAll(), ou => ou.UserId, u => u.Id, (ou, user) => user)
                                );
            return users;
        }

        public List<User> GetUsersNotInOrganizationUnitWithPage(PagedResultRequestDto paged, long organizationUnitId)
        {
            var users = GetUsersNotInOrganizationUnit(organizationUnitId)
                        .Skip(paged.SkipCount)
                        .Take(paged.MaxResultCount);
            return users.ToList();
        }

        public int GetUsersNotInOrganizationUnitCount(long organizationUnitId)
        {
            return GetUsersNotInOrganizationUnit(organizationUnitId).Count();
        }

    }
}
