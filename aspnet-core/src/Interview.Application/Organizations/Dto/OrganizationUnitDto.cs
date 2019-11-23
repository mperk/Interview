﻿using Abp.Application.Services.Dto;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace Interview.Organizations.Dto
{
    public class OrganizationUnitDto : EntityDto<long>
    {
        [Required]
        [StringLength(OrganizationUnit.MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

    }
}
