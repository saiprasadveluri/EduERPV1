using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<StudentYearStreamMapDTO> GetStudentYearStreamMapByCourseId(Guid id)
        {
            return _unitOfWork.StudentYearStreamMapRepo.GetByParentId(id);
        }
    }
}
