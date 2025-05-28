using DocumentFormat.OpenXml.Spreadsheet;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EduERPApi.RepoImpl
{
    public class AccountRepoImpl : IRepo<LoginDataDTO>, IRawRepo<LoginDataDTO, LoginResultDTO>
    {

        EduERPDbContext _context;

        public AccountRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(LoginDataDTO item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public LoginDataDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, LoginDataDTO item)
        {
            throw new NotImplementedException();
        }


        public (LoginResultDTO,bool) GetLoginResult(LoginDataDTO inp)
        {
            LoginResultDTO loginResultDTO = new LoginResultDTO();
            bool Success = false;
            var LoggedInUser= _context.UserInfos.Include(u => u.UserOrgMapList).ThenInclude(om => om.CurOrganization)
                .Where(ui => ui.UserEmail == inp.Email && ui.Password == inp.Password).FirstOrDefault();
            if(LoggedInUser!=null)
            {
                Success = true;
                loginResultDTO.UserId= LoggedInUser.UserId;
                loginResultDTO.UserEmail= inp.Email;
                loginResultDTO.UserDetailsJson = LoggedInUser.UserDetailsJson;

                loginResultDTO.UserOrgMapInfos = new List<UserOrgMapDTO>();

                foreach (var usermap in LoggedInUser.UserOrgMapList)
                {
                    UserOrgMapDTO usermapDto = new UserOrgMapDTO()
                    {
                        OrgId = usermap.OrgId,
                        OrgName = usermap.CurOrganization.OrgName,
                        
                    };
                    loginResultDTO.UserOrgMapInfos.Add(usermapDto);
                }
            }
            return (loginResultDTO, Success);            
        }
        public LoginResultDTO ExecuteRaw(LoginDataDTO inp)
        {
            var userEmail = new SqlParameter("UserEmail", inp.Email);
            var password = new SqlParameter("Password", inp.Password);

            var SelUserObj=_context.UserInfos.FromSql($"Select * from UserInfos where UserEmail={userEmail} AND Password={password}").FirstOrDefault();
            
            if (SelUserObj != null)
            {
                LoginResultDTO loginResultDTO = new LoginResultDTO();
                var Res = (from uorgMapObj in _context.UserOrgMaps
                           join OrgObj in _context.Organizations on uorgMapObj.OrgId equals OrgObj.OrgId
                           join UserInfoObj in _context.UserInfos on uorgMapObj.UserId equals UserInfoObj.UserId
                           where uorgMapObj.UserId == SelUserObj.UserId
                           select new UserOrgMapDTO()
                           {
                               UserOrgMapId= uorgMapObj.UserOrgMapId,
                               OrgId = OrgObj.OrgId,
                               OrgName = OrgObj.OrgName,
                               UserId= uorgMapObj.UserId,
                               UserDisplayName= UserInfoObj.DisplayName
                           }).ToList();
                loginResultDTO.UserId= SelUserObj.UserId;
                loginResultDTO.UserOrgMapInfos = Res;
                loginResultDTO.UserEmail = inp.Email;
                return loginResultDTO;
            }
            else
            {
                return null;
            }
        }
    }
}
