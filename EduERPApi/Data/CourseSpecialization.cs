using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class CourseSpecialization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CourseSpecializationId { get; set; }
        [Required]
        public Guid MainCourseId { get; set; }
        [Required]
        [StringLength(50)]
        public string SpecializationName { get; set;}
        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }
        //Navigation Props
        public MainCourse ParentCourse { get; set; }
        public IList<CourseDetail> CourseDetailList { get; set; }
    }
}
