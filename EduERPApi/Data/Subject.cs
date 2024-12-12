using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SubjectId { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [Required]
        public string SubjectName { get; set; }

        [Required]
        public string SubjCode { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }

        [Required]
        [DefaultValue(0)]
        public int IsLanguageSubject { get; set; }

        public int? LanguageNumber { get; set; }
        
        //Nav Props
        public Organization ParentOrganization { get; set; }
        public IList<StreamSubjectMap> StreamSubjectMapsList { get; set; }
        public IList<SubjectTopic> SubjectTopicList { get; set; }
    }
}
