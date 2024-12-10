using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class OrganizationController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        public OrganizationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Res=_unitOfWork.OrganizationRepo.GetAll();
                return Ok(new { Status = 1, Data = Res });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Status = 0, Data = 201, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrganizationDTO inp)
        {
            try
            {
                var Res = _unitOfWork.OrganizationRepo.Add(inp);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = 0, Data = 202, Message = ex.Message });
            }
        }
    }
}
