using EduERPApi.Data;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class StudentExamScheduleMapRepoImpl:IRawRepo<Guid,bool>
    {
        EduERPDbContext _context;

        public StudentExamScheduleMapRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public bool ExecuteRaw(Guid ExamId)
        {
            //Get the Current Exam Instence
            var CurExam = _context.Exams.FirstOrDefault(ex => ex.ExamId == ExamId);
                
            if(CurExam!=null)
            {
                //Delete the existing Map.
                var ExamSchsIds = _context.ExamSchedules.Where(sh => sh.ExamId == ExamId).Select(sh => sh.ExamScheduleId).ToList();
                var ExistingSchMaps = _context.StudentExamScheduleMaps.Where(sch => ExamSchsIds.Contains(sch.ExamScheduleId)).ToList();
                if(ExistingSchMaps.Count>0)
                {
                    _context.StudentExamScheduleMaps.RemoveRange(ExistingSchMaps);
                    _context.SaveChanges();
                }
                var AcdYearId = CurExam.AcdYearId;
                var CourseDetId = CurExam.CourseDetialId;
                //Fetch the Exam Schedules under the Exam

                var NonLanguageSubjectSchedulesData = (from obj in _context.ExamSchedules
                                         join ssmapObj in _context.StreamSubjectMaps on obj.StreamSubjectMapId equals ssmapObj.StreamSubjectMapId
                                         join subObj in _context.Subjects on ssmapObj.SubjectId equals subObj.SubjectId
                                         join cdObj in _context.CourseDetails on ssmapObj.StreamId equals cdObj.CourseDetailId
                                         join stuMapObj in _context.StudentYearStreamMaps on cdObj.CourseDetailId equals stuMapObj.CourseStreamId
                                         where stuMapObj.AcdYearId== AcdYearId && obj.ExamId== ExamId && subObj.IsLanguageSubject!=1
                                         select new
                                         {
                                             ExamScheduleId= obj.ExamScheduleId,
                                             StudentMapId= stuMapObj.StudentYearStreamMapId,
                                             SubjectTitle= subObj.SubjectName,
                                             
                                         }).ToList();

                var LanguageSubjectSchedulesData = (from obj in _context.ExamSchedules
                                                       join ssmapObj in _context.StreamSubjectMaps on obj.StreamSubjectMapId equals ssmapObj.StreamSubjectMapId
                                                       join subObj in _context.Subjects on ssmapObj.SubjectId equals subObj.SubjectId
                                                       join cdObj in _context.CourseDetails on ssmapObj.StreamId equals cdObj.CourseDetailId
                                                       join stuMapObj in _context.StudentYearStreamMaps on cdObj.CourseDetailId equals stuMapObj.CourseStreamId
                                                       join stuLangObj in _context.StudentLanguages on stuMapObj.StudentYearStreamMapId equals stuLangObj.StudentYearStreamMapId
                                                       where stuMapObj.AcdYearId == AcdYearId && obj.ExamId == ExamId && subObj.IsLanguageSubject == 1
                                                       select new
                                                       {
                                                           ExamScheduleId = obj.ExamScheduleId,
                                                           StudentMapId = stuMapObj.StudentYearStreamMapId,
                                                           SubjectTitle = $"Language - {subObj.LanguageNumber.ToString()} [{subObj.SubjectName}]",
                                                           
                                                       }).ToList();

                var ExamSchedulesData = NonLanguageSubjectSchedulesData.Union(LanguageSubjectSchedulesData);

                foreach (var finalRec in ExamSchedulesData)
                {
                    StudentExamScheduleMap obj = new StudentExamScheduleMap()
                    {
                        ExamScheduleId= finalRec.ExamScheduleId,
                        StudentExamScheduleMapId = Guid.NewGuid(),
                        SubjectTitle = finalRec.SubjectTitle,
                        StudentYearStreamMapId = finalRec.StudentMapId
                    };
                    _context.StudentExamScheduleMaps.Add(obj);
                    _context.SaveChanges();
                }
                return true;
            }
            return false;
        }

        private static string SubjectDisplayName(string SubjectName,int? LangSeqNo,Guid? StudentLangId)
        {
            if(!StudentLangId.HasValue)
            {
                return SubjectName;
            }
            else
            {
                return "Language : " + LangSeqNo.Value.ToString();
            }
        }
    }
}
