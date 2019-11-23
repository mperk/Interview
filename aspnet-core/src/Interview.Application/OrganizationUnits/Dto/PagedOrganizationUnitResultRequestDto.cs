using Abp.Application.Services.Dto;

namespace Interview.OrganizationUnits.Dto
{
    public class PagedOrganizationUnitResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

