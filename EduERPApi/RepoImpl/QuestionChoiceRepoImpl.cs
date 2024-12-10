using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class QuestionChoiceRepoImpl:IRepo<QuestionChoiceDTO>, IRawRepo<Guid, string>
    {
        EduERPDbContext _context;

        public QuestionChoiceRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(QuestionChoiceDTO item)
        {
            QuestionChoice choice = new QuestionChoice()
            {
                   OptId=Guid.NewGuid(),
                   ChDescription = item.ChDescription,
                   QuestionId=item.QuestionId,
                   IsCorrect=item.IsCorrect
            };
            _context.QuestionChoices.Add(choice);
            return choice.OptId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public QuestionChoiceDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, QuestionChoiceDTO item)
        {
            throw new NotImplementedException();
        }

        public List<QuestionChoiceDTO> GetByParentId(Guid questionId)
        {
            var Res = (from cobj in _context.QuestionChoices
                       join qobj in _context.Questions on cobj.QuestionId equals qobj.QID
                       where cobj.QuestionId == questionId
                       select new QuestionChoiceDTO()
                       {
                           OptId = cobj.OptId,
                           ChDescription = cobj.ChDescription,
                           IsCorrect = cobj.IsCorrect
                       }).ToList();
            return Res;
        }

        
    }
}
