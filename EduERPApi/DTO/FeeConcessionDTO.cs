using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class FeeConcessionDTO
    {
        public Guid ConId { get; set; }
        public Guid FeeId { get; set; }
        public Guid MapId { get; set; }
        public double Amount { get; set; }

        public int ConcessionType { get; set; }

        public string Reason { get; set; }
    }
}
