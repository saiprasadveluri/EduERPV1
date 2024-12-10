using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class StreamSubjectMapDTO
    {
        public Guid? StreamSubjectMapId { get; set; }
        
        public Guid SubjectId { get; set; }
        
        public Guid StreamId { get; set; }

        public string SubjectName { get; set; }
    }
}
