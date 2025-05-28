using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<ModuleFeatureDTO> GetAllModuleFeatures(Guid moduleId)
        {
           return  _unitOfWork.ModuleFeatureRepo.GetByParentId(moduleId);
        }
    }
}
