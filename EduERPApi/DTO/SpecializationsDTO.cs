using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class SpecializationsDTO
    {
        public Guid CourseSpecializationId { get; set; }
        [Required]
        public Guid MainCourseId { get; set; }
        [Required]
        [MaxLength(50)]
        public string SpecializationName { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
