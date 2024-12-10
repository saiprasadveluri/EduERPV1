using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgUserFeatureRoleMapController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        public OrgUserFeatureRoleMapController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddMap(AppUserFeatureRoleMapDTO inp)
        {
            string ErrorMessage = "";
            try
            {
                //Validate Feature-User-Org Relation
                var Obj = _unitOfWork.FeatureRoleRepo.GetById(inp.FeatureRoleId);
                if (Obj != null)
                {
                    var ReqFeatureId=Obj.FeatureId;


                    var MapObj = _unitOfWork.UserOrgMapRepoImpl.GetById(inp.OrgUserMapId);
                    if (MapObj!=null)
                    {
                        List<OrgnizationFeatureSubscriptionDTO> Lst=_unitOfWork.OrgnizationFeatureSubscriptionRepo.GetByParentId(MapObj.OrgId);
                        if(Lst.Count>0)
                        {
                            var SubMapObj = Lst.Find(f => f.FeatureId == ReqFeatureId);
                            if(SubMapObj!=null)
                            {
                                Guid newMapId = _unitOfWork.AppUserFeatureRoleMapRepo.Add(inp);
                                _unitOfWork.SaveAction();
                                return Ok(new { Status = 1, Data = newMapId });
                            }
                            else
                            {
                                ErrorMessage = "The required feature is not subscribed by your Organization";
                            }
                        }
                    }  
                    else
                    {
                        ErrorMessage = "No User found with given ID";
                    }
                }
                else
                {
                    ErrorMessage = "No Feature found with given ID";
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = 0, Data = ex.Message });
            }

            return BadRequest(new { Status = 0, Data = ErrorMessage });
        }        
    }
}
