using EduERPApi.DTO;
using EduERPApi.RepoImpl;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<AcdYearDTO> GetAllAcdYears()
        {
            var data = _unitOfWork.AcdYearRepo.GetAll().Select(r => new AcdYearDTO()
            {
                AcdYearId = r.AcdYearId,
                AcdYearText = r.AcdYearText,
            }).ToList();
            return data;
        }
    }
}
