using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class QuestionChoiceDTO
    {
        public Guid? OptId { get; set; }
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public string ChDescription { get; set; }
        public string ChImageTitle { get; set; }
        public IFormFile formFile { get; set; }
        public string ChImageBase64String { get; set; }       
        [Required]
        public int IsCorrect { get; set; }
    }
}
