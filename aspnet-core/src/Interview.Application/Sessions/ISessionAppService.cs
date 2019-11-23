using System.Threading.Tasks;
using Abp.Application.Services;
using Interview.Sessions.Dto;

namespace Interview.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
