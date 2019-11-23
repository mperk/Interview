using Abp.Authorization;
using Interview.Authorization.Roles;
using Interview.Authorization.Users;

namespace Interview.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
