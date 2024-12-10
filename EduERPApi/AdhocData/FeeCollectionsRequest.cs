namespace EduERPApi.AdhocData
{
    public class FeeCollectionsRequest
    {
        public Guid MapId { get; set; }
    }

    public class FeeCollectionsResponse
    {
        public Guid FID { get; set;}
        public double Amount { get; set; }
    }
}
