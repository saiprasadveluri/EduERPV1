using EduERPApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
   
    public class OrganizationDTO
    {
        public Guid OrgId { get; set; }
        [Required]
        public Guid OrgModuleType { get; set; }
        public string? ModuleTypeText { get; set; }
        [Required]
        [StringLength(50)]
        public string OrgName { get; set; }
        [Required]
        [StringLength(500)]
        public string OrgAddress { get; set; }
        [Required]
        [StringLength(50)]
        public string PrimaryEmail { get; set; }
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [Required]
        public int Status { get; set; } = 1;

    }
}
