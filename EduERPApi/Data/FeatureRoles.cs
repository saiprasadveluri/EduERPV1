using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class FeatureRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AppRoleId { get; set; }
        [Required]
        public Guid FeatureId { get; set; }
        [Required]
        [StringLength(150)]
        public string RoleName { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }

        //Navigation Props
        public ModuleFeature ParentFeature { get; set; }   
        public IList<AppUserFeatureRoleMap> UserFeatureRoleMapList { get; set; }
    }
}
