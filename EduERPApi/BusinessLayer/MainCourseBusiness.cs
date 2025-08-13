using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<MainCourseDTO> GetCoursesByOrgId()
        {
            var OrgId = Guid.Parse(_context.GetSession<string>("OrgId"));
            var Res = _unitOfWork.MainCourseRepo.GetByParentId(OrgId);
            return Res;
        }

        public (Guid,bool) AddNewMainCourse(MainCourseDTO course)
        {
            var SelOrgId = Guid.Parse(_context.GetSession<string>("OrgId"));
            course.OrgId = SelOrgId;
            Guid NewCourseId = _unitOfWork.MainCourseRepo.Add(course);
            if (course.IsSpecializationsAvailable == 0)
            {
                Guid NewSplId = _unitOfWork.CourseSpecialzationsRepo.Add(new SpecializationsDTO()
                {
                    MainCourseId = NewCourseId,
                    SpecializationName = course.CourseName,
                    Status = 1
                });
                for (int year = 1; year <= course.DurationInYears; ++year)
                {
                    for (int term = 1; term <= course.NumOfTermsInYear; ++term)
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
                _unitOfWork.SaveAction();
                return (NewCourseId, true);
            }
            else
            {
                _unitOfWork.SaveAction();
                return (NewCourseId, true);
            }
        }
    }
}
