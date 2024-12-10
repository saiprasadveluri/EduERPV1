using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class QuestionDTO
    {
        
        public Guid? QID { get; set; }

        [Required]
        public Guid TopicID { get; set; }

        public string TopicTitle { get; set; }
        
        public string QDescription { get; set; }

        public string QImageTitle { get; set; }
        public Guid FileGuid { get; set; }
        public IFormFile formFile { get; set; }

       
        public int QComplexity { get; set; }

       
        public double Mark { get; set; }

       

    }
}
