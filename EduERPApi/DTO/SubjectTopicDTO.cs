using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class SubjectTopicDTO
    {
        public Guid? SubTopicID { get; set; }
        [Required]
        public string TopicName { get; set; }
        [Required]
        public string TopicCode { get; set; }
        [Required]
        public Guid SubId { get; set; }

        public string SubjectName { get; set; }

        [Required]        
        public int Status { get; set; }
    }
}
