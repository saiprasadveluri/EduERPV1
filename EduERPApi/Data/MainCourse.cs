using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class MainCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MainCourseId { get; set; }
        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [StringLength(500)]
        [Required]
        [DefaultValue(0)]
        public int IsSpecializationsAvailable { get; set; }

        [Required]
        [DefaultValue(1)]
        public int DurationInYears { get; set; }

        [Required]
        [DefaultValue(1)]
        public int TermsInEachYear { get; set; }
        public string Description { get; set; }
        [Required]
        public int Status { get; set; }

        //Navigation Prop
        public Organization ParentOrganization { get; set; }
        public IList<CourseSpecialization> Specializations { get; set; }
    }
}
