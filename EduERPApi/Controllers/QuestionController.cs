using DocumentFormat.OpenXml.Office2010.Excel;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.RepoImpl;
using FileImportLibrary.DTO;
using FileImportLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        IHostEnvironment _env;
        public QuestionController(UnitOfWork unitOfWork,IHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        [HttpGet("ByTopic/{id}")]
        public IActionResult GetAllByTopic(Guid id)
        {
            try
            {
                var Res=_unitOfWork.QuestionRepo.GetByParentId(id);
                return Ok(new { Status = 1, Data = Res });
            }
            catch(Exception exp)
            {

            }
            return BadRequest(new { Status = 0, Data = 1901, Message = "Error In Getting Question List" });
        }
        [HttpGet("GetImage/{id}")]
        public IActionResult GetImage(Guid id)
        {
            try
            {
                var CurRec = _unitOfWork.QuestionRepo.GetById(id);
                if(CurRec!=null && CurRec.FileGuid!=Guid.Empty)
                {
                    string BaseFolderPath = _env.ContentRootPath;
                    string FileGuidString = CurRec.FileGuid.ToString();
                   byte[] ImageBytes= FileLoadandSave.ReadContent(BaseFolderPath, FileGuidString);
                   string ImgBase64=Convert.ToBase64String(ImageBytes);
                    return Ok(new { Status = 1, Data = ImgBase64 });
                }
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 1905, Message = "Error In Fetching Question Image" });
        }
        [HttpPost("Image")]
        public IActionResult UpdateImage([FromForm]Guid Qid, [FromForm]IFormFile formFile)
        {
            Guid newGuid = Guid.NewGuid();
            try
            {
                string BaseFolderPath = _env.ContentRootPath;
                var CurRec = _unitOfWork.QuestionRepo.GetById(Qid);
                if (formFile!=null && CurRec != null && CurRec.FileGuid != Guid.Empty)
                {

                    FileLoadandSave.DeleteFile(BaseFolderPath, CurRec.FileGuid.ToString());                    
                }
                if(CurRec!=null && formFile != null)
                {
                    CurRec.FileGuid = newGuid;
                    //Save the New Image
                    FileLoadandSave.WriteContent(BaseFolderPath, CurRec.FileGuid.ToString(),formFile.OpenReadStream());
                    _unitOfWork.SaveAction();
                    return Ok(new { Status = 1, Data = newGuid });
                }

            }
            catch(Exception e)
            {
                string BaseFolderPath = _env.ContentRootPath;
                FileLoadandSave.DeleteFile(BaseFolderPath, newGuid.ToString());
            }
            return BadRequest(new { Status = 0, Data = 1905, Message = "Error In Updating Question Image" });
        }
        [HttpPost("BulkUpload")]
        public IActionResult BulkAdd(BulkQuestionUploadDTO inp)
        {
            try
            {
                if (inp.inpFile != null)
                {
                    var importComponent = new EntityImport<ParsedQuestionInfo>();
                    QuestionExcelParser parser = new QuestionExcelParser(inp.inpFile.OpenReadStream());
                    var ParsedQuestionInfoList = importComponent.ReadContent();
                    if (ParsedQuestionInfoList.Count > 0)
                    {
                        foreach (var questionInfo in ParsedQuestionInfoList)
                        {
                            QuestionDTO question = new QuestionDTO()
                            {
                                TopicID = inp.TopicId,
                                QID = Guid.NewGuid(),
                                QDescription = questionInfo.QuestionText,
                                QComplexity = questionInfo.QComplexity,
                                Mark = questionInfo.Mark
                            };
                            _unitOfWork.QuestionRepo.Add(question);
                            //Options
                            foreach(var opt in questionInfo.Options)
                            {
                                QuestionChoiceDTO questionChoice = new QuestionChoiceDTO()
                                {
                                    QuestionId = question.QID.Value,
                                    ChDescription = opt.OptionText,
                                    IsCorrect = opt.IsCorrect?1:0,
                                    OptId = Guid.NewGuid()
                                };
                                _unitOfWork.QuestionChoiceRepo.Add(questionChoice);
                            }
                        }
                        _unitOfWork.SaveAction();
                        return Ok(new { Status = 1, Data = "Success" });
                    }
                }
            }
            catch (Exception exp)
            {

            }
            return BadRequest(new { Status = 0, Data = 1906, Message = "Error In Upload" });
        }
        [HttpPost]
        public IActionResult Add(QuestionDTO item)
        {
            Guid FileGuid = Guid.NewGuid();
            string BaseFolderPath = _env.ContentRootPath;
            try
            {
                item.FileGuid = FileGuid;
                var Res = _unitOfWork.QuestionRepo.Add(item);
                if(item.formFile!=null)
                {
                    FileLoadandSave.WriteContent(BaseFolderPath, FileGuid.ToString(),item.formFile.OpenReadStream());
                }                
                _unitOfWork.SaveAction();
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception exp)
            {
                if(System.IO.File.Exists(BaseFolderPath+"/"+ FileGuid))
                {
                    System.IO.File.Delete(BaseFolderPath + "/" + FileGuid);
                }
            }
            return BadRequest(new { Status = 0, Data = 1902, Message = "Error In Adding Question" });
        }
    }
}
