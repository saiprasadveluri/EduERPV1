using EduERPApi.DTO;
using EduERPApi.RepoImpl;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
       public  bool MapExamToStudents(NewStudentExamScheduleMapRequestDTO inp)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var Status= _unitOfWork.StudentExamScheduleMapRepo.ExecuteRaw(inp.ExamId);
                _unitOfWork.CommitTransaction();
                return true;
            }
            catch(Exception e)
            {
                _unitOfWork.RollbackTransaction();
            }
            return false;
        }
    }
}
