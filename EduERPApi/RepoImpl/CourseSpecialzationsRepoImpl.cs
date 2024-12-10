using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class CourseSpecialzationsRepoImpl : IRepo<SpecializationsDTO>
    {

        EduERPDbContext _context;
        public CourseSpecialzationsRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(SpecializationsDTO item)
        {
            CourseSpecialization courseSpecialization = new CourseSpecialization()
            {
                 CourseSpecializationId=Guid.NewGuid(),
                 MainCourseId=item.MainCourseId,
                 SpecializationName=item.SpecializationName,
                 Status = item.Status
            };
            _context.CourseSpecializations.Add(courseSpecialization);
            return courseSpecialization.CourseSpecializationId;
        }

        public List<SpecializationsDTO> GetByParentId(Guid parentId)
        {
            return _context.CourseSpecializations.Where(cs => cs.MainCourseId == parentId).Select(cs =>
            new SpecializationsDTO()
            {
                CourseSpecializationId = cs.CourseSpecializationId,
                SpecializationName = cs.SpecializationName,

            }).ToList();
        }

        public bool Delete(Guid key)
        {
            var SplRecord = _context.CourseSpecializations.Where(s => s.CourseSpecializationId == key).FirstOrDefault();
            if(SplRecord != null)
            {
                _context.CourseSpecializations.Remove(SplRecord);
                return true;
            }
            return false;
        }

        public SpecializationsDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, SpecializationsDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
