using EduERPApi.Infra;
using FileImportLibrary.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduERPApi.DTO
{
    public class StudentInfoDTO
    {
        public Guid? OrgId { get; set; }
        public Guid? StudentId { get; set; }

        [Required]
        [StringLength(50)]
        public string RegdNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        //[JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateOfBirth { get; set; }

        [Required]
        //[JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateOfJoining { get; set; }

        
        public Guid UserId { get; set; }

        public string AddlDataJson { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public int Status { get; set; }
        [Required]
        public Guid StreamId { get; set; }
        [Required]
        public Guid AcdYearId { get; set; }
        public List<StudentLanguagesDTO>? LangData { get; set; }
        public List<ParsedLangData>? parsedLangData {  get; set; }
    }
}
