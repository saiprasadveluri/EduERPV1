using EduERPApi.DTO;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<SubjectTopicDTO> GetAllSubjectTopics(Guid SubjectId)
        {
            return _unitOfWork.SubjectTopicRepo.GetByParentId(SubjectId);
        }

        public (Guid, bool) AddSubjectTopic(SubjectTopicDTO subjectTopic)
        {
           Guid newSubjectTopicId= _unitOfWork.SubjectTopicRepo.Add(subjectTopic);
            bool Status= _unitOfWork.SaveAction();
            return (newSubjectTopicId, Status);
        }

        public bool DeleteSubjectTopic(Guid subjectTopicId)
        {
            bool ActionStatus = false;
            bool DelStatus=_unitOfWork.SubjectTopicRepo.Delete(subjectTopicId);
            if (DelStatus)
            {
                ActionStatus = _unitOfWork.SaveAction();
            }            
            return ActionStatus;
        }
    }
}
