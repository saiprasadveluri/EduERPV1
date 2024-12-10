using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class AppUserFeatureRoleMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AppUserRoleMapId { get; set; }

        [Required]
        public Guid UserOrgMapId { get; set; }
        

        [Required]
        public Guid FeatureRoleId { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }


        //Navigation Props
        public FeatureRole ParentFeatureRole { get; set; }
        public UserOrgMap ParentUserOrgMap { get; set; }
        
        //public UserInfo ParentUser { get; set; }

    }
}
