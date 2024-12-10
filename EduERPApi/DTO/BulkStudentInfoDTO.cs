namespace EduERPApi.DTO
{
    public class BulkStudentInfoDTO
    {
        public Guid? OrgId { get; set; }
        public Guid? StreamId { get; set; }
        public Guid? AcdYearId { get; set; }
        public IFormFile inpFile { get; set; }
    }
}
