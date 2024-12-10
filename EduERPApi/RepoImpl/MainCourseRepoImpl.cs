using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using System.Linq;
namespace EduERPApi.RepoImpl
{
    public class MainCourseRepoImpl : IRepo<MainCourseDTO>
    {
        EduERPDbContext _context;
        public MainCourseRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public List<MainCourseDTO> GetByParentId(Guid parentId)
        {
            return _context.MainCourses.Where(mc => mc.OrgId == parentId).Select(rec =>            
                new MainCourseDTO()
                {
                    MainCourseId = rec.MainCourseId,
                    CourseName = rec.CourseName,
                    IsSpecializationsAvailable = rec.IsSpecializationsAvailable,
                    DurationInYears = rec.DurationInYears,
                    NumOfTermsInYear = rec.TermsInEachYear
                }
            ).ToList();
        }
        public Guid Add(MainCourseDTO item)
        {
            MainCourse mainCourse = new MainCourse()
            {
                MainCourseId=Guid.NewGuid(),
                CourseName = item.CourseName,
                DurationInYears = item.DurationInYears,
                TermsInEachYear=item.NumOfTermsInYear,
                Description = item.Description,
                OrgId = item.OrgId.Value,
                IsSpecializationsAvailable = item.IsSpecializationsAvailable,                   
                Status = item.Status
            };
            _context.MainCourses.Add(mainCourse);
            return mainCourse.MainCourseId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public MainCourseDTO GetById(Guid id)
        {
            return _context.MainCourses.Where(m => m.MainCourseId == id).Select(m => new MainCourseDTO()
            {
                MainCourseId = m.MainCourseId,
                CourseName = m.CourseName,
                DurationInYears = m.DurationInYears,
                Description = m.Description,
                IsSpecializationsAvailable = m.IsSpecializationsAvailable,
                NumOfTermsInYear = m.TermsInEachYear
            }).FirstOrDefault();

        }

        public bool Update(Guid key, MainCourseDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
