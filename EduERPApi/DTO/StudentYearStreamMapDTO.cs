using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class StudentYearStreamMapDTO
    {
        public Guid? StudentYearStreamMapId { get; set; }
        public Guid? StudentId { get; set; }
        public string StudentName { get; set; }

        [Required]
        public Guid AcdYearId { get; set; }
        [Required]
        public Guid CourseStreamId { get; set; }
    }
}
