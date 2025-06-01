using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class UserInfoDTO
    {
        public Guid? UserId { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [Required]
        [MaxLength(150)]
        public string UserEmail { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        [MaxLength(500)]
        public string DisplayName { get; set; }

        public List<Guid>? FeatureRoleList { get; set; }

        [Required]
        public int IsOrgAdmin { get; set; }

        public string UserDetailsJson { get; set; }
        
    }
}
