using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class FeeConcession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ConId { get; set; }
        public Guid FeeId { get; set; }
        public Guid MapId { get; set; }
        public double Amount { get; set; }

        [Required]
        public int ConcessionType { get; set; }

        [Required]
        public string Reason { get; set; }

        
        public FeeMaster ParentFeeId { get; set; }
        
        public StudentYearStreamMap SSMap { get; set; }
    }
}
