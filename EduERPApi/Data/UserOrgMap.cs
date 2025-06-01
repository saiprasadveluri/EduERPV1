using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class UserOrgMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserOrgMapId { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int IsOrgAdmin { get; set; }
        //Nav Props
        public Organization CurOrganization { get; set; }
        public UserInfo CurUserInfo { get; set; }
        public IList<AppUserFeatureRoleMap> CurUserRoles { get; set; }
    }
}
