using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgnizationFeatureSubscriptionController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        public OrgnizationFeatureSubscriptionController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubscription(OrgnizationFeatureSubscriptionDTO inp)
        {
            string ErrorMessage = "";
            try
            {
                //Check Org is of same Type (AppModuleType) as that of Feature
                var CurrentOrg = _unitOfWork.OrganizationRepo.GetById(inp.OrgId);
                var CurFeature = _unitOfWork.ModuleFeatureRepo.GetById(inp.FeatureId);
                if (CurFeature.ModuleId == CurrentOrg.OrgModuleType)
                {
                    OrgnizationFeatureSubscriptionDTO dto = new()
                    {
                        FeatureId = inp.FeatureId,
                        OrgId = inp.OrgId,
                        Status = inp.Status,
                        SubId = Guid.NewGuid()
                    };
                    Guid newSub=_unitOfWork.OrgnizationFeatureSubscriptionRepo.Add(dto);
                    _unitOfWork.SaveAction();
                    return Ok(new { Status = 1, Data = newSub });
                }
                else
                {
                    ErrorMessage = "Can not add the selected feature to the organization";
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = "Error In Adding Record";
            }
            return BadRequest(new { Status = 0, Data =501, Message = ErrorMessage });
        }
    }
}
