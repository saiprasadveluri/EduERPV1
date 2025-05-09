using EduERPApi.DTO;
using EduERPApi.RepoImpl;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        
        public List<UserInfoDTO> GetAllUsersByOrognization(Guid Id)
        {
            return _unitOfWork.UserInfoRepo.GetByParentId(Id);
        }

        public ((Guid,Guid),bool) AddUserInfo(UserInfoDTO inp)
        {
            Guid NewUserId = _unitOfWork.UserInfoRepo.Add(inp);
            Guid UserOrgMapId = _unitOfWork.UserOrgMapRepoImpl.Add(
                new UserOrgMapDTO()
                {
                    OrgId = inp.OrgId,
                    UserId = NewUserId
                });
            bool Status=_unitOfWork.SaveAction();
            return ((NewUserId, UserOrgMapId), Status);
        }
    }
}
