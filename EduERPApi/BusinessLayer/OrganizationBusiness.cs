using EduERPApi.Data;
using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public (Guid,bool) AddOrganization(OrganizationDTO item)
        {
            var Res = _unitOfWork.OrganizationRepo.Add(item);
            _unitOfWork.SaveAction();
            return (Res,true);
        }

        public List<OrganizationDTO> GetAllOrganizations()
        {
            var Res = _unitOfWork.OrganizationRepo.GetAll();
            return Res;
        }

        public OrganizationDTO GetOrganizationById(Guid OrgId)
        {
            var Res = _unitOfWork.OrganizationRepo.GetById(OrgId);
            return Res;
        }
    }
}
