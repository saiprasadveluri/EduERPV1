using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduERPApi.Data
{
    public class StudentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid StudentId { get; set; }

        [Required]
        public Guid OrgId { get; set; }

        [Required]
        [StringLength(50)]
        public string RegdNumber { get; set;}

        [Required]
        [StringLength(50)]
        public string Name { get; set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public string? AddlDataJson { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        //Nav Props
        public UserInfo CurUserInfo { get; set; }
        public Organization ParentOrganization { get; set; }
        public IList<StudentYearStreamMap> StudentCourseStreams { get; set; }        

    }
}
