using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class CourseDetailDTO
    {
        public Guid? CourseDetailId { get; set; }
        [Required]
        public Guid SpecializationId { get; set; }
        [Required]
        public int Year { get; set; } = 1;
        [Required]

        public int Term { get; set; } = 1;
    }
}
