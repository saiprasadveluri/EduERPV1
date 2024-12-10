using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class ExamScheduleRepoImpl : IRepo<ExamScheduleDTO>
    {

        EduERPDbContext _context;

        public ExamScheduleRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(ExamScheduleDTO item)
        {
            ExamSchedule exSch = new ExamSchedule()
            {
              ExamScheduleId=Guid.NewGuid(),
              StreamSubjectMapId=item.StreamSubjectMapId,
              ExamDate=item.ExamDate,
              ExamTime=item.ExamTime,
              ExamOrderNo=item.ExamOrderNo,
              ExamId=item.ExamId,
              ExamPaperFileId=item.ExamPaperId,
              Notes=item.Notes              
            };
            _context.ExamSchedules.Add(exSch);
            return exSch.ExamScheduleId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public ExamScheduleDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, ExamScheduleDTO item)
        {
            throw new NotImplementedException();
        }

        public List<ExamScheduleDTO> GetByParentId(Guid ExamId)
        {
            var Res = (from obj in _context.ExamSchedules
                       join ssobj in _context.StreamSubjectMaps on obj.StreamSubjectMapId equals ssobj.StreamSubjectMapId
                       join ssub in _context.Subjects on ssobj.SubjectId equals ssub.SubjectId
                       where obj.ExamId == ExamId
                       select new ExamScheduleDTO()
                       {
                           ExamScheduleId = obj.ExamScheduleId,
                           ExamDate = obj.ExamDate,
                           ExamTime = obj.ExamTime,
                           ExamOrderNo = obj.ExamOrderNo,
                           Notes = obj.Notes,
                           ExamId = obj.ExamId,
                           SubjectName = ssub.SubjectName
                       }).ToList();
            return Res;
        }
    }
}
