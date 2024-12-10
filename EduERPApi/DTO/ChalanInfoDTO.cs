namespace EduERPApi.DTO
{
    public class ChalanInfoDTO
    {
        public Guid FID { get; set; }
        public string HN { get; set; }
        public int TermNo { get; set; }
        public double TotAmt { get; set; }
        public double Concession { get; set; }
        public double Paid { get; set; }
        public double Due { get; set; }
        public int DueMon { get; set; }
    }
}
