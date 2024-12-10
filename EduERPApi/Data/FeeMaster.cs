using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class FeeMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FeeId { get; set; }

        [Required]
        public Guid FHeadId { get; set; }

        [Required]
        public int TermNo { get; set; }

        public Guid? CourseDetailId { get; set; }
        public Guid? MapId { get; set; }//Student-Course-DetailsMap ID

        [Required]
        public double Amount { get; set; }

        [Required]
        public Guid AcdyearId { get; set; }

        [Required]
        public int DueDayNo { get; set; }

        [Required]
        public int DueMonthNo { get; set; }

        [ForeignKey(nameof(CourseDetailId))]
        public CourseDetail ParentCousreDetId { get; set; }
        
        [ForeignKey(nameof(MapId))]
        public StudentYearStreamMap SSMap { get; set; }

        
        [ForeignKey(nameof(AcdyearId))]
        public AcdYear AcdYear { get; set; }

        public FeeHeadMaster ParentFeeHead { get; set; }

        public IList<ChalanLineInfo> ChalanLines { get; set; }

        public IList<FeeConcession > FeeConcessions { get; set; }

        public IList<FeeCollectionLineItem> FeeCollectionLineItems { get; set; }
    }
}
