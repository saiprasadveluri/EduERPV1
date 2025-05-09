using DocumentFormat.OpenXml.Math;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class SubjectRepoImpl : IRepo<SubjectDTO>
    {
        EduERPDbContext _context;

        public SubjectRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(SubjectDTO item)
        {
            Subject subject = new Subject()
            {
                OrgId = item.OrgId.Value,
                SubjectName = item.SubjectName,
                SubjCode=item.SubjectCode,
                SubjectId = Guid.NewGuid(),
                Status = 1
            };
            _context.Add(subject);
            return subject.SubjectId;
        }

        public bool Delete(Guid key)
        {
            var CurSubj=_context.Subjects.FirstOrDefault(x => x.SubjectId == key);
            if (CurSubj != null)
            {
                _context.Subjects.Remove(CurSubj);
                return true;
            }
            return false;
        }

        public SubjectDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, SubjectDTO item)
        {
            throw new NotImplementedException();
        }

        public List<SubjectDTO> GetByParentId(Guid parentId)
        {
            var res = (from sobj in _context.Subjects
                       where sobj.OrgId== parentId
                       select new SubjectDTO()
                      {
                          SubjectId = sobj.SubjectId,
                          SubjectName = sobj.SubjectName,
                          SubjectCode= sobj.SubjCode
                      }).ToList();
            return res;

        }
    }
}
