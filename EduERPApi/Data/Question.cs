using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid QID { get; set; }

        [Required]
        public Guid TopicID { get; set; }
        [Required]
        public string QTitle { get; set; }

        public string QDescription { get; set; }

        public string QImageTitle { get; set; }
        public Guid QImageGUID { get; set; }

        [Required]
        public int QComplexity { get; set; }

        [Required]
        public double Mark { get; set; }

        //Navigation
        [ForeignKey(nameof(TopicID))]
        public SubjectTopic ParentTopic { get; set; }

    }
}
