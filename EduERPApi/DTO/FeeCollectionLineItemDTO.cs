using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class FeeCollectionLineItemDTO
    {
        public Guid LineItemId { get; set; }
       
        public Guid ColId { get; set; }
       
        public Guid FeeId { get; set; }

       
        public double Amount { get; set; }
    }
}
