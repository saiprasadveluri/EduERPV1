using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class ExamScheduleDTO
    {
        public Guid? ExamScheduleId { get; set; }
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        
        public DateTime ExamDate { get; set; }
        [Required]
       
        public DateTime ExamTime { get; set; }
        [Required]
        public int ExamOrderNo { get; set; }
        public IFormFile ExamPaperFile { get; set; }
        public Guid ExamPaperId { get; set; }
        [Required]
        public Guid StreamSubjectMapId { get; set; }
        public string SubjectName { get; set; }
        public string Notes { get; set; }



    }
}
