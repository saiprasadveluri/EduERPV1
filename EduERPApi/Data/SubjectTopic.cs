using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class SubjectTopic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SubTopicID { get; set; }
        [Required]
        public string TopicName { get; set; }
        [Required]
        public string TopicCode { get; set; }
        [Required]
        public Guid SubId { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }

        [ForeignKey("SubId")]
        public Subject ParentSubject { get; set; }
        
    }
}
