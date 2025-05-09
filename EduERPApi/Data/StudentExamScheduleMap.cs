using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class StudentExamScheduleMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid StudentExamScheduleMapId { get; set; }
        [Required]
        public Guid ExamScheduleId { get; set; }
        [Required]
        public Guid StudentYearStreamMapId { get; set; }

        [Required]
        public string SubjectTitle { get; set; }

        //Nav Props
        [ForeignKey(nameof(ExamScheduleId))]
        public ExamSchedule ParentExamSchedule { get; set; }
        [ForeignKey(nameof(StudentYearStreamMapId))]
        public StudentYearStreamMap ParentStudentYearStreamMap { get; set; }
    }
}
