using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrgMapController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        public UserOrgMapController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddMap(UserOrgMapDTO inp)
        {
            try
            {
                var newMapId = _unitOfWork.UserOrgMapRepoImpl.Add(inp);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = newMapId });
            }
            catch(Exception ex)
            {
                
            }
            return BadRequest(new { Status = 0, Data = 601, Message = "Error In Adding Map" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMap(UserOrgMapDTO Newinp)
        {
            try
            {
                var Status = _unitOfWork.UserOrgMapRepoImpl.Update(Newinp.UserOrgMapId, Newinp);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = Status });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 602, Message = "Error In Updating Map" });
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMap(Guid Id)
        {
            try
            {
                var Status = _unitOfWork.UserOrgMapRepoImpl.Delete(Id);
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = Status });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 602, Message = "Error In Deleting Map" });
        }
    }
}
