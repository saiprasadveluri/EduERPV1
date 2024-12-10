using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class ChalanLineInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ChlLineId { get; set; }
        [Required]
        public Guid ChlId { get; set; }
        [Required]
        public int TermNo { get; set; }
        [Required]
        public Guid FeeId { get; set; }
        [Required]
        public string FeeHeadName { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double PaidAmt { get; set; }

        [Required]
        public int DueMon { get; set; }

       
        public Chalan ParentChln { get; set; }

        
        public FeeMaster ParentFeeMaster { get; set; }
    }
}
