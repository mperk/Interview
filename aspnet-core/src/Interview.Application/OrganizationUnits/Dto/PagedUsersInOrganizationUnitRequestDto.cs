using Abp.Application.Services.Dto;

namespace Interview.OrganizationUnits.Dto
{
    public class PagedUsersInOrganizationUnitRequestDto : PagedResultRequestDto
    {
        public long organizationUnitId { get; set; }
    }
}

