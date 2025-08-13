using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    partial class Business
    {
        public List<ApplicationModuleDTO> GetAllApplicationModule()
        {
            var data = _unitOfWork.ApplicationModuleRepo.GetAll();
            return data;
        }
    }
}
