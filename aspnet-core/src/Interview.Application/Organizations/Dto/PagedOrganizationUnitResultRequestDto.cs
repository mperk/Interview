using Abp.Application.Services.Dto;

namespace Interview.Organizations.Dto
{
    public class PagedOrganizationUnitResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

