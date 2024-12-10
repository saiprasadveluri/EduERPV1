using DocumentFormat.OpenXml.Office2010.Excel;
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
            var Rec= _context.ExamSchedules.Where(sh => sh.ExamScheduleId == key).FirstOrDefault();
            if(Rec!=null)
            {
                _context.ExamSchedules.Remove(Rec);
                return true;
            }
            return false;
        }

        public ExamScheduleDTO GetById(Guid id)
        {
            return _context.ExamSchedules.Where(sh => sh.ExamScheduleId == id).
                    Select(sh => new ExamScheduleDTO()
                    {
                        ExamScheduleId=sh.ExamScheduleId,
                        ExamPaperId=sh.ExamPaperFileId,
                        ExamDate=sh.ExamDate,
                        ExamTime=sh.ExamTime,
                        ExamId=sh.ExamId,
                        Notes=sh.Notes,
                        StreamSubjectMapId=sh.StreamSubjectMapId
                    }).FirstOrDefault();
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
                       orderby obj.ExamDate descending
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
