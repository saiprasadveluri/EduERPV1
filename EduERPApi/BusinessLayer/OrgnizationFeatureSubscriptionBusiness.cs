using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public (Guid,bool) AddOrganizationFeatureSubscription(OrgnizationFeatureSubscriptionDTO inp)
        {
            var CurrentOrg = _unitOfWork.OrganizationRepo.GetById(inp.OrgId);
            var CurFeature = _unitOfWork.ModuleFeatureRepo.GetById(inp.FeatureId);
            if (CurFeature.ModuleId == CurrentOrg.OrgModuleType)
            {
                OrgnizationFeatureSubscriptionDTO dto = new()
                {
                    FeatureId = inp.FeatureId,
                    OrgId = inp.OrgId,
                    Status = inp.Status,
                    SubId = Guid.NewGuid()
                };
                Guid newSub = _unitOfWork.OrgnizationFeatureSubscriptionRepo.Add(dto);
                _unitOfWork.SaveAction();
                return (newSub, true);
                
            }            
            return (Guid.Empty, false);
        }

        public OrganizationFeatureDTO GetModuleFeatures(Guid ModuleId)
        {
            OrganizationFeatureDTO dto = new OrganizationFeatureDTO();
            var lst = _unitOfWork.ModuleFeatureRepo.GetByParentId(ModuleId)
                .Select(entry => new OrganizationFeatureEntry()
                {
                    FeatureName = entry.FeatureName,
                    FeatureId = entry.FeatureId
                }).ToList();
            dto.FeatureList = lst;
            return dto;
        }
    }
}
