using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace EduERPApi.Data
{
    public class Chalan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ChlId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SeqNo { get; set; }
        [Required]
        public Guid OrgId { get; set; }
        [Required]        
        public string ChlnNumber { get; set; }//Computed Column
        
        [Required]
        public Guid MapId { get; set; }
        [Required]
        public DateTime ChlDate { get; set; }

        [Required]
        public int ChalanStatus { get; set; }
        [ForeignKey("MapId")]
        public StudentYearStreamMap SSMap { get; set; }

        public IList<ChalanLineInfo> ChalanLines { get; set; }
        public IList<FeeCollection> FeeCollections { get; set; }

    }
}
