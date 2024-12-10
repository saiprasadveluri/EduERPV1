using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class FeatureRoleImpl:IRepo<FeatureRoleDTO>
    {
        EduERPDbContext _context;
        public FeatureRoleImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(FeatureRoleDTO item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public FeatureRoleDTO GetById(Guid id)
        {
            return _context.FeatureRoles.Where(fr => fr.AppRoleId == id).
                Select(fr => new FeatureRoleDTO() { 
                             AppRoleId= fr.AppRoleId,
                             FeatureId= fr.FeatureId,
                             RoleName=fr.RoleName
                }).FirstOrDefault();
        }

        public bool Update(Guid key, FeatureRoleDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
