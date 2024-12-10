using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class TokenRequestDTO
    {
        
        [Required]
        public Guid SelOrgId { get; set; }
        

    }
}
