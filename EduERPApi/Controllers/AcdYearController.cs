using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcdYearController : ControllerBase
    {
        UnitOfWork _unitOfWork;
     
        public AcdYearController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;          
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data=_unitOfWork.AcdYearRepo.GetAll().Select(r => new AcdYearDTO()
                {
                    AcdYearId = r.AcdYearId,
                    AcdYearText = r.AcdYearText,
                }).ToList();
                return Ok(new { Status = 1, Data = data });
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1201, Message = "Error In Getting AcdYears" });
        }
    }
}
