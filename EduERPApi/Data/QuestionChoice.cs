using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class QuestionChoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OptId { get; set; }
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public string ChDescription { get; set; }
        public string ChImageTitle { get; set; }
        public string ChImageGUID { get; set; }

        [DefaultValue(0)]
        [Required]
        public int IsCorrect { get; set; }

        [ForeignKey("QuestionId")]
        public Question ParentQuestion { get; set; }
    }
}
