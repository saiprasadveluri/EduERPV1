using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class AcdYear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AcdYearId { get; set; }

        [Required]
        public string AcdYearText { get; set;}
        public IList<StudentYearStreamMap> StudentYearStreamMapsList { get; set; }
    }
}
