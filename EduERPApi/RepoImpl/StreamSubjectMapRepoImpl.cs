using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using System.Linq;

namespace EduERPApi.RepoImpl
{
    public class StreamSubjectMapRepoImpl : IRepo<StreamSubjectMapDTO>
    {
        EduERPDbContext _context;

        public StreamSubjectMapRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(StreamSubjectMapDTO item)
        {
            StreamSubjectMap ssmap = new StreamSubjectMap()
            {
                StreamSubjectMapId=Guid.NewGuid(),
                SubjectId=item.SubjectId,
                StreamId=item.StreamId,
            };
            _context.Add(ssmap);
            return ssmap.StreamSubjectMapId;
        }

        public bool Delete(Guid key)
        {
            var curMap = _context.StreamSubjectMaps.FirstOrDefault(s=>s.StreamSubjectMapId==key);
            if (curMap != null)
            {
                _context.StreamSubjectMaps.Remove(curMap);
                return true;
            }
            return false;
        }

        public StreamSubjectMapDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, StreamSubjectMapDTO item)
        {
            throw new NotImplementedException();
        }
        public List<StreamSubjectMapDTO> GetByParentId(Guid parentId)
        {
            var res = _context.StreamSubjectMaps.Join(_context.Subjects, s => s.SubjectId, o => o.SubjectId, (i, o) => new {inner=i,outer=o})
                .Where(r=>r.inner.StreamId==parentId)
                .Select(rec=>
                new StreamSubjectMapDTO()
                {
                    StreamSubjectMapId = rec.inner.StreamSubjectMapId,
                    SubjectId = rec.inner.SubjectId,
                    SubjectName=rec.outer.SubjectName,
                    StreamId = rec.inner.StreamId
                }).ToList();
            return res;
        }
    }
}
