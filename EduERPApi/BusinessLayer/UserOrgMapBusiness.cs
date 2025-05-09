using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public (Guid mapId,bool Status) AddUserOrgMap(UserOrgMapDTO inp)
        {
            var newMapId = _unitOfWork.UserOrgMapRepoImpl.Add(inp);
            _unitOfWork.SaveAction();
            return (newMapId,true);
        }

        public bool UpdateUserOrgMap(UserOrgMapDTO Newinp)
        {
            var Status = _unitOfWork.UserOrgMapRepoImpl.Update(Newinp.UserOrgMapId, Newinp);
            _unitOfWork.SaveAction();
            return Status;
        }

        public bool DeleteUserOrgMap(Guid Id)
        {
            var Status = _unitOfWork.UserOrgMapRepoImpl.Delete(Id);
            _unitOfWork.SaveAction();
            return Status;
        }
    }
}
