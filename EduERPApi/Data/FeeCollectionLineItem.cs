using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class FeeCollectionLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LineItemId { get; set; }
        [Required]
        public Guid ColId { get; set; }
        [Required]
        public Guid FeeId { get; set; }

        [Required]
        public double Amount { get; set; }

        
        public FeeCollection ParentFeeCollection { get; set; }

        
        public FeeMaster ParentFeeMaster { get; set; }

    }
}
