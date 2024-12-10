using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class SubjectTopicRepoImpl : IRepo<SubjectTopicDTO>
    {

        EduERPDbContext _context;

        public SubjectTopicRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(SubjectTopicDTO item)
        {
            SubjectTopic subjectTopic = new SubjectTopic()
            {
                SubTopicID = Guid.NewGuid(),
                TopicName = item.TopicName,
                TopicCode = item.TopicCode,
                SubId = item.SubId
            };
            _context.SubjectTopics.Add(subjectTopic);
            return subjectTopic.SubTopicID;
        }

        public bool Delete(Guid key)
        {
            var curTopic = _context.SubjectTopics.Where(st => st.SubTopicID == key).FirstOrDefault();
            if(curTopic!=null)
            {
                _context.SubjectTopics.Remove(curTopic);
                return true;
            }
            return false;
        }

        public SubjectTopicDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, SubjectTopicDTO item)
        {
            throw new NotImplementedException();
        }

        public List<SubjectTopicDTO> GetByParentId(Guid subjectId)
        {
            var Res = (from obj in _context.SubjectTopics
                       join subObj in _context.Subjects on obj.SubId equals subObj.SubjectId
                       where obj.SubId == subjectId
                       select new SubjectTopicDTO()
                       {
                           SubTopicID = obj.SubTopicID,
                           SubjectName = subObj.SubjectName,
                           SubId = obj.SubId,
                           Status = obj.Status,
                           TopicName = obj.TopicName,
                           TopicCode = obj.TopicCode

                       }).ToList();
            return Res;
        }
    }
}
