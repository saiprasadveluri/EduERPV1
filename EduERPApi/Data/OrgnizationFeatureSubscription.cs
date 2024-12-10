using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class OrgnizationFeatureSubscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SubId { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [Required]
        public Guid FeatureId { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }

        //Navigation Props
        public ModuleFeature CurrentFeature { get; set; }
        public Organization CurrentOrganization { get; set; }

    }
}
