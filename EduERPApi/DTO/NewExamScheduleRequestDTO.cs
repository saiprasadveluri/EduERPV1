using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class NewExamScheduleRequestDTO
    {
        [Required]
        public Guid ExamId { get; set; }
        public IEnumerable<SubjectExamScheduleDTO> SubjectExamSchedules { get; set; }
    }

    public class SubjectExamScheduleDTO
    {
        [DataType(DataType.Date)]
        public DateTime ExamDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime ExamTime { get; set; }
        
        public int ExamOrderNo { get; set; }
        public IFormFile ExamPaperFile { get; set; }
       
        public Guid StreamSubjectMapId { get; set; }

        public string Notes { get; set; }
    }
}
