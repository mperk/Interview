using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Interview.Controllers
{
    public abstract class InterviewControllerBase: AbpController
    {
        protected InterviewControllerBase()
        {
            LocalizationSourceName = InterviewConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
