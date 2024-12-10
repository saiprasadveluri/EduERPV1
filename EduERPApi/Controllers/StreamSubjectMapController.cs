using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamSubjectMapController : ControllerBase
    {
        UnitOfWork _unitOfWork;

        public StreamSubjectMapController(UnitOfWork unitOfWork, IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("ByStream/{CrsId}")]
        public IActionResult GetExistingMaps(Guid CrsId)
        {
            try
            {                
                var Existing = _unitOfWork.StreamSubjectMapRepo.GetByParentId(CrsId);
                return Ok(new { Status = 1, Data = Existing });

            }
            catch (Exception ex)
            {
            }
            return BadRequest(new { Status = 0, Data = 1701, Message = "Error In Getting Data" });
        }

        [HttpPost]
        public IActionResult CreateNewMaps(NewStreamSubjectMapsDTO dto)
        {
            try
            {
                //get and delete existing Maps
                var Existing = _unitOfWork.StreamSubjectMapRepo.GetByParentId(dto.CourseDetId);
                foreach (var emap in Existing)
                {
                    _unitOfWork.StreamSubjectMapRepo.Delete(emap.StreamSubjectMapId.Value);
                }
                foreach (var sub in dto.SubjectIdList)
                {
                    StreamSubjectMapDTO mapObj = new StreamSubjectMapDTO()
                    {
                        StreamId = dto.CourseDetId,
                        SubjectId = sub,
                        StreamSubjectMapId = Guid.NewGuid(),
                    };
                    _unitOfWork.StreamSubjectMapRepo.Add(mapObj);
                }
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = "Success" });
            }
            catch (Exception ex)
            {
            }
            return BadRequest(new { Status = 0, Data = 1801, Message = "Error In Adding new Map" });
        }
    }
}