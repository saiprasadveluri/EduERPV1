using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace EduERPApi.RepoImpl
{
    public class CourseDetailRepoImpl : IRepo<CourseDetailDTO>,IRawRepo<CourseDetailDTO,Guid>
    {
        EduERPDbContext _context;
        public CourseDetailRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(CourseDetailDTO item)
        {
            CourseDetail courseDetail = new CourseDetail()
            {
                CourseDetailId = Guid.NewGuid(),
                SpecializationId = item.SpecializationId,
                Year = item.Year,
                Term = item.Term
            };
            _context.CourseDetails.Add(courseDetail);
            return courseDetail.CourseDetailId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public CourseDetailDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, CourseDetailDTO item)
        {
            throw new NotImplementedException();
        }

        public Guid ExecuteRaw(CourseDetailDTO inp)
        {
           var DetObj= _context.CourseDetails.FromSqlInterpolated($"SELECT * from CourseDetails WHERE SpecializationId={inp.SpecializationId.ToString()} AND Year={inp.Year} AND Term={inp.Term}").FirstOrDefault();
            if(DetObj!=null)
            {
                return DetObj.CourseDetailId;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}
