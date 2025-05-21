using DocumentFormat.OpenXml.Office2010.Excel;
using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<ExamTypeDTO> GetExamTypeByCourse(Guid CourseId)
        {
            var Res = _unitOfWork.ExamTypeRepo.GetByParentId(CourseId);
            return Res;
        }

        public (Guid,bool) AddExamType(ExamTypeDTO dto)
        {
            var Res = _unitOfWork.ExamTypeRepo.Add(dto);
            bool Status=_unitOfWork.SaveAction();
            return (Res,Status);
        }
    }
}
