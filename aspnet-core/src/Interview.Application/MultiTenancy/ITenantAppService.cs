using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Interview.MultiTenancy.Dto;

namespace Interview.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

