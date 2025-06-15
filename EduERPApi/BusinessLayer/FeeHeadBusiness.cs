using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public FeeHeadMasterDTO GetFeeHeadById(Guid id)
        {
           return  _unitOfWork.FeeHeadMasterRepoImpl.GetById(id);
        }

        public (Guid,bool) AddFeeHeadMaster(FeeHeadMasterDTO inp)
        {
            string SelOrgIdString = _context.GetSession<string>("OrgId");
            Guid SelOrgId = Guid.Parse(SelOrgIdString);
            inp.OrgId = SelOrgId;
            Guid NewFeeHeadId = _unitOfWork.FeeHeadMasterRepoImpl.Add(inp);
            bool Status=_unitOfWork.SaveAction();
            return (NewFeeHeadId, Status);
        }

        public List<FeeHeadMasterDTO> GetAllFeeHeadByOrganization(Guid OrgId)
        {
           return  _unitOfWork.FeeHeadMasterRepoImpl.GetByParentId(OrgId);
        }
    }
}
