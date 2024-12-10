using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class ModuleFeature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FeatureId { get; set; }
        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }
        [Required]
        public Guid ModuleId { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }

        //Navigation Properties
        public ApplicationModule ParentModule { get; set; }
        public IList<OrgnizationFeatureSubscription> OrgFeatureSubscriptions { get; set; }
        public IList<FeatureRole> CurrentFeatureRoles { get; set; }
    }
}
