using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class ExamTypeDTO
    {        
        public Guid? ExamTypeId { get; set; }
        [Required]
        public string ExamTypeName { get; set; }
        [Required]
        public Guid MainCourseId { get; set; }
    }
}
