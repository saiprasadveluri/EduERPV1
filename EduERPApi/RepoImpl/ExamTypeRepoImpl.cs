using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class ExamTypeRepoImpl : IRepo<ExamTypeDTO>
    {

        EduERPDbContext _context;

        public ExamTypeRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(ExamTypeDTO item)
        {
            ExamType et = new ExamType
            {
                ExamTypeId=Guid.NewGuid(),
                ExamTypeName = item.ExamTypeName,
                MainCourseId = item.MainCourseId
            };
            _context.ExamTypes.Add(et);
            return et.ExamTypeId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public ExamTypeDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, ExamTypeDTO item)
        {
            throw new NotImplementedException();
        }

        public List<ExamTypeDTO> GetByParentId(Guid parentId)
        {
            var Res = _context.ExamTypes.Where(et => et.MainCourseId == parentId).Select(et =>
            
            new ExamTypeDTO()
            {
                ExamTypeId = et.ExamTypeId,
                ExamTypeName = et.ExamTypeName,
            }).ToList();
            return Res;
       
        }
    }
}
