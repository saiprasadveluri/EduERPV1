using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<SubjectDTO> GetAllSubjectsByOrganization(Guid SelOrgId)
        {
            return _unitOfWork.SubjectRepo.GetByParentId(SelOrgId);
        }

        public (Guid,bool) AddSubject(SubjectDTO subject)
        {
            Guid SubjectId=_unitOfWork.SubjectRepo.Add(subject);
            bool Result=_unitOfWork.SaveAction();
            if (Result)
                return (SubjectId, true);
            else
                return (Guid.Empty, false);

        }
    }
}
