using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class FeatureRoleDTO
    {
        public Guid AppRoleId { get; set; }
        [Required]
        public Guid FeatureId { get; set; }
        [Required]
        [MaxLength(150)]
        public string RoleName { get; set; }
        public int Status { get; set; } = 1;
    }
}
