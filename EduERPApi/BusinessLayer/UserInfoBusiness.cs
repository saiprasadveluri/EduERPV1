using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using System.Net.NetworkInformation;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        
        public List<UserInfoDTO> GetAllUsersByOrognization(Guid Id)
        {
            return _unitOfWork.UserInfoRepo.GetByParentId(Id);
        }

        public (Guid,bool) AddUserInfo(UserInfoDTO inp)
        {
            bool Status = true;
            Guid NewUserId = _unitOfWork.UserInfoRepo.Add(inp);
            Guid UserOrgMapId = _unitOfWork.UserOrgMapRepoImpl.Add(
                                new UserOrgMapDTO()
                                {
                                    OrgId = inp.OrgId,
                                    UserId = NewUserId,
                                    IsOrgAdmin = inp.IsOrgAdmin,

                                });
            if (inp.IsOrgAdmin==0)
            {                
                foreach(var FeatureRole in inp.FeatureRoleList)
                {
                    AppUserFeatureRoleMapDTO userFeatureroleDto = new AppUserFeatureRoleMapDTO()
                    {
                        AppUserRoleMapId = Guid.NewGuid(),
                        FeatureRoleId = FeatureRole,
                        OrgUserMapId = UserOrgMapId,
                        Status = 1
                    };
                    (Guid mapId,bool opStatus) Res=AddOrgUserFeatureRoleMap(userFeatureroleDto);
                    Status = Status && Res.opStatus;
                }
            }
            if (Status)
            {
                Status = _unitOfWork.SaveAction();
                return (NewUserId, Status);
            }
            return (Guid.Empty, false);
        }
    }
}
