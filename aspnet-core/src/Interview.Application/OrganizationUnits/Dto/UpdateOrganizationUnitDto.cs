using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace Interview.OrganizationUnits.Dto
{
    public class UpdateOrganizationUnitDto
    {
        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [Required]
        public long Id { get; set; }
    }
}
