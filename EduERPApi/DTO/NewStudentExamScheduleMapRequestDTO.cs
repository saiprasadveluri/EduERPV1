using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class NewStudentExamScheduleMapRequestDTO
    {
        [Required]
        public Guid ExamId { get; set; }       
    }
}
