using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class OrgnizationFeatureSubscriptionDTO
    {
        public Guid? SubId { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [Required]
        public Guid FeatureId { get; set; }
        [Required]
        public int Status { get; set; } = 1;
    }
}
