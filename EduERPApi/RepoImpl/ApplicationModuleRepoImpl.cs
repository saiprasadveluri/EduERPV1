using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class ApplicationModuleRepoImpl : IRepo<ApplicationModuleDTO>
    {
        EduERPDbContext _context;
        public ApplicationModuleRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(ApplicationModuleDTO item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public ApplicationModuleDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, ApplicationModuleDTO item)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationModuleDTO> GetAll()
        {
            return _context.ApplicationModules.Select(am => new ApplicationModuleDTO
            {
                ModuleId = am.ModuleId,
                ModuleName = am.ModuleName,
                Status = am.Status
            }).ToList();

        }
    }
}
