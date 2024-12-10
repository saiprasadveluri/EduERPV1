using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class ApplicationModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ModuleId { get; set; }
        [Required]
        [StringLength(50)]
        public string ModuleName { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }
        //Navigation Properties
        public IList<ModuleFeature> ModuleFeatures { get; set; }
        
    }
}
