using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class UserOrgMapDTO
    {
        public Guid UserOrgMapId { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public int IsOrgAdmin { get; set; }
        public string? OrgName { get; set; }
        public string? UserDisplayName { get; set; }
    }
}
