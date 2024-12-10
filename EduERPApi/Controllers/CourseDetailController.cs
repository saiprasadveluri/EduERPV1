using EduERPApi.DTO;
using EduERPApi.Repo;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseDetailController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        public CourseDetailController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Extract")]
        [EnableCors("AllowAll")]
        public IActionResult Extract(CourseDetailDTO inp) { 
            try
            {
                IRawRepo<CourseDetailDTO, Guid> repoObj = _unitOfWork.CourseDetailRepo as IRawRepo<CourseDetailDTO, Guid>;
                if (repoObj != null)
                {
                    var Res = repoObj.ExecuteRaw(inp);
                    if (Res != Guid.Empty)
                    {
                        return Ok(new { Status = 1, Data = Res });
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1401, Message = "Error In Getting Id" });
        }
    }
}
