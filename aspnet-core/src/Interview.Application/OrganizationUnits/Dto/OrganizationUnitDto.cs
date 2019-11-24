using Abp.Application.Services.Dto;
using Abp.Organizations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interview.OrganizationUnits.Dto
{
    public class OrganizationUnitDto : EntityDto<long>
    {
        [Required]
        [StringLength(OrganizationUnit.MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public long? ParentId { get; set; }

        public List<OrganizationUnitDto> Children { get; set; }

    }
}
