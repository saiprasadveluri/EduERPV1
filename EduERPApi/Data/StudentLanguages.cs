using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class StudentLanguage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid StudentLangId { get; set; }
        [Required]
        public int LangNumber { get; set; }
        [Required]
        public Guid StudentYearStreamMapId { get; set; }
        [Required]
        public Guid SubjectMapId { get; set; }
        

        [ForeignKey(nameof(StudentYearStreamMapId))]
        public StudentYearStreamMap ParentStudent { get; set; }

        [ForeignKey(nameof(SubjectMapId))]
        public StreamSubjectMap ParentSubject { get; set; }
    }
}
