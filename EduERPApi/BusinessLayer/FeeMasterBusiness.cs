using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public (Guid, bool) AddFeeMaster(FeeMasterDTO inp)
        {
            var HeadInfo = _unitOfWork.FeeHeadMasterRepoImpl.GetById(inp.FHeadId);
            if (HeadInfo != null)
            {
                inp.AddMode = HeadInfo.FeeType;
                int DuplicateRow = (_unitOfWork.FeeMasterRepoImpl as IRawRepo<FeeMasterDTO, int>).ExecuteRaw(inp);
                if (DuplicateRow == 0)
                {
                    Guid NewId = _unitOfWork.FeeMasterRepoImpl.Add(inp);
                   bool Res= _unitOfWork.SaveAction();
                    return (NewId, Res);
                }
                else
                {
                    throw new Exception("Duplicate Row:");
                }
            }
            else
            {
                throw new Exception("Error In Input data:");
            }
        }
    }
}
