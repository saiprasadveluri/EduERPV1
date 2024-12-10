using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class AcdYearRepoImpl : IRepo<AcdYearDTO>
    {
        EduERPDbContext _context;

        public AcdYearRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public List<AcdYearDTO> GetAll()
        {
            return _context.AcdYears.Select(y => new AcdYearDTO()
            {
                AcdYearId = y.AcdYearId,
                AcdYearText = y.AcdYearText,
            }).ToList();
        }
        public Guid Add(AcdYearDTO item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public AcdYearDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, AcdYearDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
