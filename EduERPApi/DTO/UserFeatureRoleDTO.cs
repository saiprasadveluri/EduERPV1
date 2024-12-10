using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class UserFeatureRoleDTO
    {
        public Guid AppUserRoleMapId { get; set; }

        [Required]
        public Guid UserOrgMapId { get; set; }

        [Required]
        public Guid FeatureRoleId { get; set; }

        public int Status { get; set; } = 1;
    }
}
