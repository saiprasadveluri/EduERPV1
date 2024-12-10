using EduERPApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class FeeHeadMasterDTO    
    {
        public Guid? FeeHeadId { get; set; }

       
        public Guid? OrgId { get; set; }

        [Required]
       public string FeeHeadName { get; set; }

        [Required]
        public int FeeType { get; set; }

        [Required]
        public int Terms { get; set; }

    }
}
