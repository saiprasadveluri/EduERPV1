using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class AppUserFeatureRoleMapDTO
    {
        public Guid AppUserRoleMapId { get; set; }

        [Required]
        public Guid OrgUserMapId { get; set; }

        [Required]
        public Guid FeatureRoleId { get; set; }

        public string FeatureName { get; set; }
        public string RoleName { get; set; }

        [Required]
        public int Status { get; set; } = 1;
        //public UserOrgMapDTO UserOrgMapObj { get; set; }
    }
}
