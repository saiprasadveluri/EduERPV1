using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileImportLibrary;
using FileImportLibrary.DTO;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        IConfiguration _cfg;
        public StudentController(UnitOfWork unitOfWork,IConfiguration cfg)
        {
            _unitOfWork = unitOfWork;
            _cfg= cfg;
        }

        [HttpPost("Bulk")]
        //[Authorize(Roles=RoleConstents.SCHOOL_STUDENT_MANAGEMENT_STUDENT_ROLE_GUID)]
        public IActionResult BulkAdd([FromForm]BulkStudentInfoDTO inp)
        {
            try
            {
                var SelOrgId=Guid.Parse(HttpContext.Session.GetString("OrgId"));
                if (inp.inpFile!=null)
                {

                    string StructureFileName=_cfg.GetValue<string>("EntityStructurePath:StudentInfoStructure");

                    var importComponent = new EntityImport<ParsedStudentInfo>(StructureFileName);

                    StudentExcelParser parser = new StudentExcelParser(inp.inpFile.OpenReadStream());
                    importComponent.SetParser(parser);
                    var StdInfoDTOList = importComponent.ReadContent();
                   //var OrgSubjList= _unitOfWork.SubjectRepo.GetByParentId(SelOrgId);
                    foreach(var stdInfoObj in  StdInfoDTOList)
                    {
                        StudentInfoDTO stuObj = new StudentInfoDTO()
                        {
                            OrgId = SelOrgId,
                            AcdYearId=inp.AcdYearId.Value,
                            StreamId=inp.StreamId.Value,
                            Email=stdInfoObj.Email,
                            Name=stdInfoObj.Name,
                            Phone=stdInfoObj.Phone,
                            Address=stdInfoObj.Address,
                            DateOfBirth = stdInfoObj.DateOfBirth,
                            DateOfJoining = stdInfoObj.DateOfJoining,
                            Status=1,
                            Password="MyPassword",
                            RegdNumber=stdInfoObj.RegdNumber,
                            parsedLangData=stdInfoObj.LangData
                        };                        
                        _unitOfWork.StudentInfoRepo.Add(stuObj);
                    }
                    _unitOfWork.SaveAction();
                    return Ok(new { Status = 1, Data = "Success" });
                }                

            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 701, Message = "Error In Adding Students" });
        }

        [HttpPost]
        //[Authorize(Roles=RoleConstents.SCHOOL_STUDENT_MANAGEMENT_STUDENT_ROLE_GUID)]
        public async Task<IActionResult> Add(StudentInfoDTO inp)
        {
            try
            {
                    inp.OrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                    Guid NewStudentId = _unitOfWork.StudentInfoRepo.Add(inp);
                    return Ok(new { Status = 1, Data = NewStudentId });                
                
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 701, Message = "Error In Adding Student"});
        }
    }
}
