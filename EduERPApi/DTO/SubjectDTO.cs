using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EduERPApi.DTO
{
    public class SubjectDTO
    {
        public Guid? SubjectId { get; set; }
        
        public Guid? OrgId { get; set; }
       
        public string SubjectName { get; set; }

        [Required]
        public string SubjectCode {  get; set; }

        public int Status { get; set; }
    }
}
