using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class FeeHeadMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FeeHeadId { get; set; }

        [Required]
        public Guid OrgId { get; set; }

        [Required]
        public string FeeHeadName { get; set; }

        [Required]
        public int FeeType { get; set; }

        [Required]
        public int Terms { get; set; }

        
        public Organization ParentOrg { get; set; }
        public IList<FeeMaster> ChildFeeRecords { get; set; }

    }
}
