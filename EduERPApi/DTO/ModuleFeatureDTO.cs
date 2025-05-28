using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class ModuleFeatureDTO
    {
        public Guid FeatureId { get; set; }
        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }
        [Required]
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        [Required]
        public int Status { get; set; } = 1;
    }
}
