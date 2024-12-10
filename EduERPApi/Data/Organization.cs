using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OrgId { get; set; }
        [Required]
        public Guid OrgModuleType { get; set; }
        [Required]
        [StringLength(50)]
        public string OrgName { get; set; }
        [Required]
        [StringLength(500)]
        public string OrgAddress { get; set; }
        [Required]
        [StringLength(50)] 
        public string PrimaryEmail { get; set; }
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [Required]
        public int Status { get; set; }
        
        //Navigation Props
        public IList<OrgnizationFeatureSubscription> OrgFeatureSubscriptions { get; set; }
        public IList<MainCourse> CurrentCourses { get; set; }
        public IList<UserOrgMap> UserOrgMapList { get; set; }
        public IList<Subject> OrgSubjects { get; set; }
        public IList<StudentInfo> OrgStudents { get; set; }
        public IList<FeeHeadMaster> OrgFeeHeads { get; set; }
    
    }
}
