using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.OrganizationUnits.Dto
{
    public class DeleteUserFromOrganizationUnitDto
    {
        public virtual int? TenantId { get; set; }
        public virtual long UserId { get; set; }
        public virtual long OrganizationUnitId { get; set; }
    }
}
