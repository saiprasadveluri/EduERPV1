using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class StudentYearStreamMapRepoImpl:IRepo<StudentYearStreamMapDTO>
    {
        EduERPDbContext _context;

        public StudentYearStreamMapRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public List<StudentYearStreamMapDTO> GetByParentId(Guid parentId)
        {
            var Temp = (from obj in _context.StudentYearStreamMaps
                        join StuObj in _context.StudentInfos on obj.StudentId equals StuObj.StudentId
                        where obj.CourseStreamId == parentId
                        select new StudentYearStreamMapDTO()
                        {
                            StudentYearStreamMapId = obj.StudentYearStreamMapId,
                            StudentId = obj.StudentId,
                            AcdYearId = obj.AcdYearId,
                            CourseStreamId = obj.CourseStreamId,
                            StudentName=StuObj.Name
                        }).ToList();
            return Temp;
            /*return _context.StudentYearStreamMaps.Where(m=>m.CourseStreamId == parentId)

                .Select(obj=>new StudentYearStreamMapDTO()
                {
                     StudentYearStreamMapId = obj.StudentYearStreamMapId,
                     StudentId = obj.StudentId,
                     AcdYearId = obj.AcdYearId,
                     CourseStreamId=obj.CourseStreamId,

                }).ToList();*/
        }
        public Guid Add(StudentYearStreamMapDTO item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public StudentYearStreamMapDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, StudentYearStreamMapDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
