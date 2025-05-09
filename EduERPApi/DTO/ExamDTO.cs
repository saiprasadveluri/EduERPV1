using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class ExamDTO
    {       
        public Guid? ExamId { get; set; }
        [Required]
        public string ExamTitle { get; set; }
        [Required]
        public Guid ExamTypeId { get; set; }

        public string? ExamType { get; set; }
        [Required]
        public Guid CourseDetialId { get; set; }
        [Required]
        
        public DateTime StartDate { get; set; }
        [Required]
        
        public DateTime EndDate { get; set; }
        [Required]
        public Guid AcdYearId { get; set; }
        public string? AcdYearText { get; set; }

    }
}
