using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class StudentLanguagesDTO
    {
        public Guid? StudentLangId { get; set; }
        [Required]
        public int LangNumber { get; set; }
        [Required]
        public Guid StudentYearStreamMapId { get; set; }
        [Required]
        public Guid SubjectMapId { get; set; }
    }
}
