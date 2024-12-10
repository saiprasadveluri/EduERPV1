using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class FeeCollectionDTO
    {
        public Guid FeeColId { get; set; }

     
        public Guid ChlnId { get; set; }

        public int PayType { get; set; }

        public string Notes { get; set; }

      
        public DateTime ColDate { get; set; }
    }
}
