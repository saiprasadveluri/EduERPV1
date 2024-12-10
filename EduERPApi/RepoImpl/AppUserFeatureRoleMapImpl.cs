using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class AppUserFeatureRoleMapImpl : IRepo<AppUserFeatureRoleMapDTO>
    {

        EduERPDbContext _context;
        public AppUserFeatureRoleMapImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(AppUserFeatureRoleMapDTO item)
        {
            AppUserFeatureRoleMap mapObj = new AppUserFeatureRoleMap()
            {
                AppUserRoleMapId = Guid.NewGuid(),
                UserOrgMapId = item.OrgUserMapId,
                FeatureRoleId = item.FeatureRoleId,
                Status = 1
            };
            _context.AppUserFeatureRoleMaps.Add(mapObj);
            return mapObj.AppUserRoleMapId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public AppUserFeatureRoleMapDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<AppUserFeatureRoleMapDTO> GetByParentId(Guid UserOrgMapId)
        {
            return (from obj in _context.AppUserFeatureRoleMaps 
                    join froles in _context.FeatureRoles on obj.FeatureRoleId equals froles.AppRoleId
                    join featureobj in _context.ModuleFeatures on froles.FeatureId equals featureobj.FeatureId
             where obj.UserOrgMapId== UserOrgMapId
             select new AppUserFeatureRoleMapDTO()
             {    
                 FeatureName= featureobj.FeatureName,
                 RoleName= froles.RoleName,
                 FeatureRoleId = obj.FeatureRoleId
             }).ToList();            
        }

        public bool Update(Guid key, AppUserFeatureRoleMapDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
