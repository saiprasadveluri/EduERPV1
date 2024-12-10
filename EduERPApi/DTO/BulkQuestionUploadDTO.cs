using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class BulkQuestionUploadDTO
    {
        [Required]
        public Guid TopicId { get; set; }
        public IFormFile inpFile { get; set; }
    }
}
