using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class StudentYearStreamMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid StudentYearStreamMapId { get; set; }

        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public Guid AcdYearId { get; set; }
        [Required]
        public Guid CourseStreamId { get; set;}

        //Nav Props
        public StudentInfo ParentStudent { get; set; }
        public CourseDetail ParentCourseStream { get; set; }
        public AcdYear ParentAcdYear { get; set; }
        public IList<FeeConcession> FeeConcessions { get; set;}
        public IList<FeeCollection> FeeCollections { get; set; }
    }
}
