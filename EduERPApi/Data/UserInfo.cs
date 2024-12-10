using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(150)]
        public string UserEmail  { get; set; }
        [Required]
        [StringLength(20)]
        public string Password   { get; set; }
        [Required]
        [StringLength (50)]
        public string DisplayName { get; set; }
        public string? UserDetailsJson { get; set; }
        
        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }
        //Navigation Props        
        //public List<AppUserFeatureRoleMap> CurUserRoles { get; set; }
        public List<UserOrgMap> UserOrgMapList { get; set; }
        public List<StudentInfo> StudentInfoList { get; set;}
    }
}
