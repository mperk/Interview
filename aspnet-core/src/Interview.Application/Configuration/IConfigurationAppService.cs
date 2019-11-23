using System.Threading.Tasks;
using Interview.Configuration.Dto;

namespace Interview.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
