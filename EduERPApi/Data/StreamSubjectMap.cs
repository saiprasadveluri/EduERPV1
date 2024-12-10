using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class StreamSubjectMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid StreamSubjectMapId { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        public Guid StreamId { get; set; }

        [DefaultValue(0)]
        public int IsOptional { get; set; }

        //Nav Props
        public CourseDetail ParentStream { get; set; }
        public Subject ParentSubject { get; set; }
    }
}
