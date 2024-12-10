using EduERPApi.Data;

namespace EduERPApi.DTO
{
    public class LoginResultDTO
    { 
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public List<UserOrgMapDTO> UserOrgMapInfos { get; set;}
        public string JwtToken { get; set; } 
        public string HeaderUserId { get; set; }
        public string HeaderPassword { get; set; }
    }
}
