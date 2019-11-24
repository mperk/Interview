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

        public async Task<List<User>> GetUsersInOrganizationUnitAsync(PagedResultRequestDto paged, long organizationUnitId)
        {
            var users = (await _userOrganizationUnitRepository.GetAllListAsync())
                .Where(x => x.OrganizationUnitId == organizationUnitId)
                .Join(await _userRepository.GetAllListAsync(), ou => ou.UserId, user => user.Id, (ou, user) => user)
                .Skip(paged.SkipCount)
                .Take(paged.MaxResultCount);
            return users.ToList();
        }

        public async Task<List<User>> GetUsersNotInOrganizationUnitAsync(long organizationUnitId)
        {
            var users = (await _userOrganizationUnitRepository.GetAllListAsync())
                .Where(x => x.OrganizationUnitId == organizationUnitId)
                .Join(await _userRepository.GetAllListAsync(), ou => ou.UserId, u => u.Id, (ou, user) => user)
                .Except(await _userRepository.GetAllListAsync());
            return users.ToList();
        }

        //public async Task<List<User>> AddUserInOrganizationUnit()
        //{
        //    var users = (await _userOrganizationUnitRepository
        //        .GetAllListAsync())
        //        .Join(_userRepository.GetAll(), ou => ou.UserId, u => u.Id, (ou, user) => user);
        //    return users.ToList();
        //}

        //public async Task<List<User>> DeleteUserInOrganizationUnit()
        //{
        //    var users = (await _userOrganizationUnitRepository
        //        .GetAllListAsync())
        //        .Join(_userRepository.GetAll(), ou => ou.UserId, u => u.Id, (ou, user) => user);
        //    return users.ToList();
        //}
    }
}
