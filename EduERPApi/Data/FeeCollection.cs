using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.Data
{
    public class FeeCollection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid FeeColId { get; set; }

        //[Required]
        //public Guid MapId { get; set; }

        [Required]
        public Guid ChlnId { get; set; }

        [Required]
        public int PayType { get; set; }

        public string Notes { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ColDate { get; set; }
        //public StudentYearStreamMap SSMap { get; set; }
        public Chalan ParentChalan { get; set; }

        public IList<FeeCollectionLineItem> FeeCollectionLineItems { get; set; }

    }
}
