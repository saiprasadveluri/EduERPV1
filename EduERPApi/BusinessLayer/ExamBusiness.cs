using DocumentFormat.OpenXml.Office2010.Excel;
using EduERPApi.Data;
using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public ExamDTO GetExamById(Guid ExamId)
        {
            var Res = _unitOfWork.ExamRepo.GetById(ExamId);
            return Res;
        }

        public List<ExamDTO> GetExamByCourseId(Guid CourseDetialId)
        {
            var Res = _unitOfWork.ExamRepo.GetByParentId(CourseDetialId);
            return Res;
        }

        public (Guid,bool) AddExam(ExamDTO inp)
        {
            var Res = _unitOfWork.ExamRepo.Add(inp);
            bool Status=_unitOfWork.SaveAction();
            return (Res, Status);
        }
    }
}
