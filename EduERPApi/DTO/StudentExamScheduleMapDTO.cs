using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class StudentExamScheduleMapDTO
    {
        public Guid? StudentExamScheduleMapId { get; set; }
        [Required]
        public Guid ExamScheduleId { get; set; }
        [Required]
        public Guid StudentYearStreamMapId { get; set; }
        public string SubjectName { get; set; }
    }
}
