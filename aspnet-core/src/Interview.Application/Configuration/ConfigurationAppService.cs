using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Interview.Configuration.Dto;

namespace Interview.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : InterviewAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
