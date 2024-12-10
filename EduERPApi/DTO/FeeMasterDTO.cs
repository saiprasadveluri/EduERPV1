using EduERPApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class FeeMasterDTO
    {
        public Guid? FeeId { get; set; }

        [Required]
        public Guid FHeadId { get; set; }

        [Required]
        public int TermNo { get; set; }

        public Guid? CourseDetailId { get; set; }
        public List<Guid>? MapId { get; set; }//Student-Course-DetailsMap ID

        [Required]
        public double Amount { get; set; }

        [Required]
        public Guid AcdyearId { get; set; }

        [Required]
        public int DueDayNo { get; set; }

        [Required]
        public int DueMonthNo { get; set; }

        public int AddMode { get; set; }
    }
}
