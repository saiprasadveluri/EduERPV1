using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class UserFeatureRoleIImpl : IRepo<UserFeatureRoleDTO>
    {
        EduERPDbContext _context;
        public UserFeatureRoleIImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(UserFeatureRoleDTO item)
        {
            var MapInfo = new AppUserFeatureRoleMap()
            {
                 AppUserRoleMapId=Guid.NewGuid(),
                 UserOrgMapId=item.UserOrgMapId,
                 FeatureRoleId=item.FeatureRoleId,
                 Status=item.Status,
            };
            _context.AppUserFeatureRoleMaps.Add(MapInfo);
            return MapInfo.AppUserRoleMapId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public List<UserFeatureRoleDTO> GetAll(Guid? Id)
        {
            throw new NotImplementedException();
        }

        public UserFeatureRoleDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, UserFeatureRoleDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
