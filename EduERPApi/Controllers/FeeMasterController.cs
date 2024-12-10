using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.Repo;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeMasterController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        IConfiguration _cfg;

        public FeeMasterController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
            _cfg = cfg;
        }
        [HttpPost]
        //[Authorize(Roles = RoleConstents.SCHOOL_FEE_ADMIN_ROLE_GUID)]
        public IActionResult Add(FeeMasterDTO inp)
        {
            try
            {
                var HeadInfo = _unitOfWork.FeeHeadMasterRepoImpl.GetById(inp.FHeadId);
                if(HeadInfo!=null)
                {
                    inp.AddMode = HeadInfo.FeeType;
                   int DuplicateRow= (_unitOfWork.FeeMasterRepoImpl as IRawRepo<FeeMasterDTO,int>).ExecuteRaw(inp);
                    if(DuplicateRow==0)
                    {
                        Guid NewId = _unitOfWork.FeeMasterRepoImpl.Add(inp);
                        _unitOfWork.SaveAction();
                        return Ok(new { Status = 1, Data = NewId });
                    }  
                    else
                    {
                        return BadRequest(new { Status = 0, Data = 1102, Message = "Duplicate Row" });
                    }
                }
                
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1101, Message = "Error In Adding Fee" });
        }

        
    }
}
