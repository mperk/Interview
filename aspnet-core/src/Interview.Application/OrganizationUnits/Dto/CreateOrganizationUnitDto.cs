using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace Interview.OrganizationUnits.Dto
{
    public class CreateOrganizationUnitDto
    {
        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public virtual long? ParentId { get; set; }
    }
}
