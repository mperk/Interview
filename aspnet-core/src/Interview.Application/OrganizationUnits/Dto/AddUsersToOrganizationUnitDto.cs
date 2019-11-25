using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interview.OrganizationUnits.Dto
{
    public class AddUsersToOrganizationUnitDto
    {
        [Required]
        public List<long> UserIds { get; set; }

        [Required]
        public long OrganizationUnitId { get; set; }
    }
}
