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
                            //_unitOfWork.SaveAction();
                            return ( newMapId,true);
                        }
                    }
                }
            }
            return (Guid.Empty, false);
        }

        public List<Guid> GetAllUserAllowedFeatures(Guid OrgId,Guid UserId)
        {
            var UserOrgMapId = _unitOfWork.UserOrgMapRepoImpl.GetSelUserOrgMapIdObj(new UserOrgInfoDTO()
            {
                OrgId=OrgId,
                UserId=UserId
            });
           var lst= _unitOfWork.AppUserFeatureRoleMapRepo.GetByParentId(UserOrgMapId);
            return lst.Select(mp => mp.FeatureRoleId).ToList();
        }
    }
}
