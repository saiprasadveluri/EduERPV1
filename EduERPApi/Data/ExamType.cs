using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class ExamType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ExamTypeId { get; set; }
        [Required]
        public string ExamTypeName { get; set; }
        [Required]
        public Guid MainCourseId { get; set; }

        [ForeignKey(nameof(MainCourseId))]
        public MainCourse ParentCourse { get; set; }

    }
}
