using DocumentFormat.OpenXml.Office2010.Excel;
using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<SpecializationsDTO> GetSpecialzationsByCourse(Guid CousreId)
        {
            var Res = _unitOfWork.CourseSpecialzationsRepo.GetByParentId(CousreId);
            return Res;
        }

        public bool AddSpecialization(SpecializationsDTO inp)
        {
            var mainCourse = _unitOfWork.MainCourseRepo.GetById(inp.MainCourseId);
            if (mainCourse != null && mainCourse.IsSpecializationsAvailable == 1)
            {
                var NewSplId = _unitOfWork.CourseSpecialzationsRepo.Add(inp);

                for (int year = 1; year <= mainCourse.DurationInYears; ++year)
                {
                    for (int term = 1; term <= mainCourse.NumOfTermsInYear; ++term)
                    {
                        _unitOfWork.CourseDetailRepo.Add(new CourseDetailDTO()
                        {
                            CourseDetailId = Guid.NewGuid(),
                            SpecializationId = NewSplId,
                            Year = year,
                            Term = term
                        });
                    }
                }
                bool Status = _unitOfWork.SaveAction();
                return Status;
            }
            return false;
        }

        public bool DeleteSpecialization(Guid SplId)
        {
            return _unitOfWork.CourseSpecialzationsRepo.Delete(SplId);
        }
    }
}
