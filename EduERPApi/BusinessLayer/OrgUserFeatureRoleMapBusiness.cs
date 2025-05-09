using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public (Guid,bool) AddOrgUserFeatureRoleMap(AppUserFeatureRoleMapDTO inp)
        {
            var Obj = _unitOfWork.FeatureRoleRepo.GetById(inp.FeatureRoleId);
            if (Obj != null)
            {
                var ReqFeatureId = Obj.FeatureId;
                var MapObj = _unitOfWork.UserOrgMapRepoImpl.GetById(inp.OrgUserMapId);
                if (MapObj != null)
                {
                    List<OrgnizationFeatureSubscriptionDTO> Lst = _unitOfWork.OrgnizationFeatureSubscriptionRepo.GetByParentId(MapObj.OrgId);
                    if (Lst.Count > 0)
                    {
                        var SubMapObj = Lst.Find(f => f.FeatureId == ReqFeatureId);
                        if (SubMapObj != null)
                        {
                            Guid newMapId = _unitOfWork.AppUserFeatureRoleMapRepo.Add(inp);
                            _unitOfWork.SaveAction();
                            return ( newMapId,true);
                        }
                    }
                }
            }
            return (Guid.Empty, false);
        }
    }
}
