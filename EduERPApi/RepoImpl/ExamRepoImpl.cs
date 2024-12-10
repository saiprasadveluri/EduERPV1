using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class ExamRepoImpl : IRepo<ExamDTO>
    {
        EduERPDbContext _context;

        public ExamRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(ExamDTO item)
        {
            Exam exm = new Exam()
            {
                ExamId = Guid.NewGuid(),
                ExamTypeId = item.ExamTypeId,
                ExamTitle = item.ExamTitle,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                CourseDetialId = item.CourseDetialId
            };
            _context.Add(exm);
            return exm.ExamId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public ExamDTO GetById(Guid id)
        {
            var Res = (from obj in _context.Exams
                       join extObj in _context.ExamTypes on obj.ExamTypeId equals extObj.ExamTypeId
                       where obj.ExamId == id
                       select new ExamDTO()
                       {
                           ExamId = obj.ExamId,
                           ExamType = extObj.ExamTypeName,
                           StartDate = obj.StartDate,
                           EndDate = obj.EndDate,
                           ExamTitle = obj.ExamTitle,
                           CourseDetialId = obj.CourseDetialId

                       }).FirstOrDefault();
            return Res;
        }

        public bool Update(Guid key, ExamDTO item)
        {
            throw new NotImplementedException();
        }

        public List<ExamDTO> GetByParentId(Guid parentId)
        {
            var res = (from obj in _context.Exams
                       join ext in _context.ExamTypes on obj.ExamTypeId equals ext.ExamTypeId
                       where obj.CourseDetialId == parentId
                       select new ExamDTO
                       {
                           ExamId = obj.ExamId,
                           ExamTypeId = obj.ExamTypeId,
                           ExamType = ext.ExamTypeName,
                           StartDate = obj.StartDate,
                           EndDate = obj.EndDate,
                           ExamTitle = obj.ExamTitle


                       }).ToList();
            return res;
        }
    }
}
