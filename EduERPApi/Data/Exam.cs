using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ExamId { get; set; }
        [Required]
        public string ExamTitle { get; set; }
        [Required]
        public Guid ExamTypeId { get; set; }
        [Required]
        public Guid CourseDetialId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }



        [ForeignKey(nameof(ExamTypeId))]
        public ExamType ParentExamType { get; set; }

        [ForeignKey(nameof(CourseDetialId))]
        public CourseDetail ParentCourse { get; set; } 
    }
}
