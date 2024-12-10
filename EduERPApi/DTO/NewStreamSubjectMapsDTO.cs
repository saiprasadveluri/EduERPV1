using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class NewStreamSubjectMapsDTO
    {
        [Required]
        public Guid CourseDetId  { get; set; }
        public List<Guid> SubjectIdList { get; set; }
    }
}
