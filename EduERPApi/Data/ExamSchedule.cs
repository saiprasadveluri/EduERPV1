using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class ExamSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ExamScheduleId { get; set; }
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExamDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime ExamTime { get; set; }
        [Required]
        public int ExamOrderNo { get; set; }        
        public Guid ExamPaperFileId { get; set; }
        [Required]
        public Guid StreamSubjectMapId { get; set; }
        public string Notes { get; set; }


        [ForeignKey(nameof(StreamSubjectMapId))]
        public StreamSubjectMap ParentCourseSubject { get; set; }
        [ForeignKey(nameof(ExamId))]
        public Exam ParentExam { get; set; }

    }
}
