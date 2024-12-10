using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class UserInfoRepoImpl : IRepo<UserInfoDTO>
    {
        EduERPDbContext _context;
        public UserInfoRepoImpl(EduERPDbContext context) 
        { 
        _context = context;
        }
        public Guid Add(UserInfoDTO item)
        {
            UserInfo userInfo = new UserInfo()
            {
                UserId = Guid.NewGuid(),
                UserEmail = item.UserEmail,
                Password = item.Password,
                DisplayName = item.DisplayName,
                UserDetailsJson = item.UserDetailsJson
            };
            _context.UserInfos.Add(userInfo);

            

            return userInfo.UserId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public List<UserInfoDTO> GetByParentId(Guid OrgId)
        {   
            var Res = (from obj in _context.UserInfos
                       join MapObj in _context.UserOrgMaps on obj.UserId equals MapObj.UserId
                       where MapObj.OrgId== OrgId
                        select new UserInfoDTO
                        {
                            DisplayName=obj.DisplayName,
                            UserId=obj.UserId,
                            UserDetailsJson=obj.UserDetailsJson,
                            UserEmail=obj.UserEmail,
                        }).ToList();
            return Res;
        }

        public UserInfoDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, UserInfoDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
