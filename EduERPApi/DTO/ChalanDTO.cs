namespace EduERPApi.DTO
{
    public class ChalanDTO
    {
        public Guid ChlnId { get; set; }
        public string ChlnNum { get; set; }
        public Guid OrgId { get; set; }
        public Guid StdId { get; set; }
        public string AcdYear { get; set; }
        public Guid MapId { get; set; }
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public string Stndardname { get; set; }
        public List<ChalanInfoDTO> info { get; set; } = new List<ChalanInfoDTO>();
    }
}
