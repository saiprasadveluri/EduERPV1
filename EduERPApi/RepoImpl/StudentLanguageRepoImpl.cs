using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class StudentLanguageRepoImpl : IRepo<StudentLanguagesDTO>
    {
        EduERPDbContext _context;

        public StudentLanguageRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(StudentLanguagesDTO item)
        {
            StudentLanguage studentLanguage = new StudentLanguage()
            {
                StudentYearStreamMapId = item.StudentYearStreamMapId,
                SubjectMapId = item.SubjectMapId,
                LangNumber = item.LangNumber,
                StudentLangId = Guid.NewGuid()
            };
            _context.StudentLanguages.Add(studentLanguage);
            return studentLanguage.StudentLangId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public StudentLanguagesDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, StudentLanguagesDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
