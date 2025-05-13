namespace EduERPApi.DTO
{
    public class OrganizationFeatureDTO
    {
        public List<OrganizationFeatureEntry> FeatureList = new List<OrganizationFeatureEntry>();

    }

    public class OrganizationFeatureEntry
    {
        public Guid FeatureId { get; set; }
        public string FeatureName { get; set; }
    }
        
}
