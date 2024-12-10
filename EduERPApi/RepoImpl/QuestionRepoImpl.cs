using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class QuestionRepoImpl : IRepo<QuestionDTO>
    {
        EduERPDbContext _context;

        public QuestionRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(QuestionDTO item)
        {
            Question newQuestion = new Question()
            {
                QID = Guid.NewGuid(),
                QTitle = item.QDescription,
                QDescription = item.QDescription,
                QComplexity = item.QComplexity,
                QImageGUID = item.FileGuid,
                QImageTitle = item.QImageTitle,
                Mark = item.Mark,
                TopicID = item.TopicID
            };
            _context.Questions.Add(newQuestion);
            return newQuestion.QID;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public QuestionDTO GetById(Guid id)
        {
            var Rec = _context.Questions.Where(q => q.QID == id).Select(obj => new QuestionDTO()
            {
                QID = obj.QID,
                QDescription = obj.QTitle,                
                QComplexity = obj.QComplexity,
                Mark = obj.Mark,                
                FileGuid=obj.QImageGUID
            }).FirstOrDefault();
            return Rec;
        }

        public bool Update(Guid key, QuestionDTO item)
        {
            throw new NotImplementedException();
        }

        public List<QuestionDTO> GetByParentId(Guid topicId)
        {
            var Res = (from obj in _context.Questions
                       join tobj in _context.SubjectTopics on obj.TopicID equals tobj.SubTopicID
                       where obj.TopicID == topicId
                       select new QuestionDTO()
                       {
                            QID=obj.QID,                            
                           TopicTitle=tobj.TopicName,
                           QComplexity=obj.QComplexity,
                           Mark=obj.Mark,
                           QDescription=obj.QDescription,
                           TopicID=tobj.SubTopicID

                       }).ToList();
            return Res;
        }
    }
}
