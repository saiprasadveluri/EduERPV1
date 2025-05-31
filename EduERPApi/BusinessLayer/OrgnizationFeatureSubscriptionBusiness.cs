using EduERPApi.DTO;

using System.Collections.Generic;
using System.Text.Json;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public bool AddOrganizationFeatureSubscription(List<OrgnizationFeatureSubscriptionDTO> inp)
        {
            foreach (var entry in inp)
            {
                var CurrentOrg = _unitOfWork.OrganizationRepo.GetById(entry.OrgId);
                var CurFeature = _unitOfWork.ModuleFeatureRepo.GetById(entry.FeatureId);
                if (CurFeature.ModuleId == CurrentOrg.OrgModuleType)
                {
                    OrgnizationFeatureSubscriptionDTO dto = new()
                    {
                        FeatureId = entry.FeatureId,
                        OrgId = entry.OrgId,
                        Status = 1,
                        SubId = Guid.NewGuid()
                    };
                    Guid newSub = _unitOfWork.OrgnizationFeatureSubscriptionRepo.Add(dto);
                }
            }
                bool Status=_unitOfWork.SaveAction();
            return Status;
        }

        public List<OrgnizationFeatureSubscriptionDTO> GetAllOrgnizationFeatureSubscriptions(Guid OrgId)
        {
            var Res=_unitOfWork.OrgnizationFeatureSubscriptionRepo.GetByParentId(OrgId);
            return Res;
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

        public bool RemoveFeatureToOrg(string UnsubDataJSONString)
        {            
            bool Status = true;
            var Data = JsonSerializer.Deserialize<List<UnSubscribeFeatureData>>(UnsubDataJSONString);
            foreach(var Obj in Data)
            {
                bool opStatus=_unitOfWork.OrgnizationFeatureSubscriptionRepo.DeleteBulkByOrganization(Obj);
                Status = Status && opStatus;
            }
            if(Status)
            {
                return _unitOfWork.SaveAction();
            }
            return false;            
        }
    }
}
