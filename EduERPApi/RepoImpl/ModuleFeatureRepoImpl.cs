using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class ModuleFeatureRepoImpl:IRepo<ModuleFeatureDTO>
    {
        EduERPDbContext _context;
        public ModuleFeatureRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(ModuleFeatureDTO item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public ModuleFeatureDTO GetById(Guid id)
        {
            return _context.ModuleFeatures.Where(f => f.FeatureId == id)
                .Select(f => new ModuleFeatureDTO
                {
                    FeatureId = id,
                    ModuleId=f.ModuleId,
                    FeatureName=f.FeatureName,
                    Status=f.Status
                }).FirstOrDefault();

        }

        public bool Update(Guid key, ModuleFeatureDTO item)
        {
            throw new NotImplementedException();
        }

        public List<ModuleFeatureDTO> GetByParentId(Guid moduleId)
        {
            var Res=(from obj in _context.ModuleFeatures
                    join mobj in _context.ApplicationModules on obj.ModuleId equals mobj.ModuleId
                    where obj.ModuleId==moduleId
                    select new ModuleFeatureDTO()
                    {
                        FeatureId = obj.FeatureId,
                        FeatureName = obj.FeatureName,
                        ModuleName = mobj.ModuleName
                }).ToList();
            return Res;
            
        }
    }
}
