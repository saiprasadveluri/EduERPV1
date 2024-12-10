using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel;

namespace EduERPApi.Data
{
    public class CourseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CourseDetailId { get; set; }
        [Required]
        public Guid SpecializationId { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Year { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Term { get; set; }
        //Navigation Props
        public CourseSpecialization ParentSpecialization { get; set; }
        public IList<StreamSubjectMap> StreamSubjectMapsList { get; set; }
        public IList<StudentYearStreamMap> StudentYearStreamMapList { get; set; }
    }
}
