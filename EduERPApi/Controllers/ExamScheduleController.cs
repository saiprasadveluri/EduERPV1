﻿using DocumentFormat.OpenXml.Vml;
using EduERPApi.DTO;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using EduERPApi.Infra;
using EduERPApi.Data;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamScheduleController : ControllerBase
    {
        UnitOfWork _unitOfWork;
        IHostEnvironment _env;
        const string EXAM_PAPSERS_FILE_FOLDER = "ExamPapers";
        public ExamScheduleController(UnitOfWork unitOfWork, IHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        [HttpGet("File/{SchId}")]
        public IActionResult GetQuestionPaper(Guid SchId)
        {
            try
            {
                var SelSch = _unitOfWork.ExamScheduleRepo.GetById(SchId);
                if (SelSch != null && SelSch.ExamPaperId != Guid.Empty)
                {
                    string BaseFolderPath = _env.ContentRootPath;
                    string FilePath = $"{BaseFolderPath}/{EXAM_PAPSERS_FILE_FOLDER}";
                    byte[] content = FileLoadandSave.ReadContent(FilePath, SelSch.ExamPaperId.ToString());
                    string ContentString = Convert.ToBase64String(content);
                    return Ok(new { Status = 1, Data = ContentString });
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2104, Message = "Error In Getting Question Paper" });
        }
        [HttpGet("ByExamId/{ExamId}")]
        public IActionResult GetByExamId(Guid ExamId)
        {
            try
            {

                var Res = _unitOfWork.ExamScheduleRepo.GetByParentId(ExamId);
                return Ok(new { Status = 1, Data = Res });
            }
            catch (Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2101, Message = "Error In Getting Data" });
        }
        [HttpPost]
        public IActionResult AddSchedule([FromForm] NewExamScheduleRequestDTO dto)
        {
            Guid CurQuestionPaperGuid = Guid.Empty;
            List<string> SavedFilesGuid = new List<string>();
            string BaseFolderPath = _env.ContentRootPath;
            string FilePath = $"{BaseFolderPath}/{EXAM_PAPSERS_FILE_FOLDER}";
            try
            {
                //Validate The Exam.
                var CurExam = _unitOfWork.ExamRepo.GetById(dto.ExamId);
                if (CurExam != null)
                {
                    _unitOfWork.BeginTransaction();
                    foreach (var esItem in dto.SubjectExamSchedules)
                    {
                        if (esItem.ExamDate < CurExam.StartDate || esItem.ExamDate > CurExam.EndDate)
                        {
                            throw new Exception("ERROR IN DATES...");
                        }

                        CurQuestionPaperGuid = Guid.NewGuid();
                        ExamScheduleDTO curItem = new ExamScheduleDTO()
                        {
                            ExamId = CurExam.ExamId.Value,
                            ExamDate = esItem.ExamDate,
                            ExamTime = esItem.ExamTime,
                            ExamOrderNo = esItem.ExamOrderNo,
                            Notes = esItem.Notes,
                            ExamPaperId = CurQuestionPaperGuid,
                            StreamSubjectMapId = esItem.StreamSubjectMapId
                        };
                        _unitOfWork.ExamScheduleRepo.Add(curItem);
                        _unitOfWork.SaveAction();
                        FileLoadandSave.WriteContent(FilePath, CurQuestionPaperGuid.ToString(), esItem.ExamPaperFile.OpenReadStream());
                        SavedFilesGuid.Add(CurQuestionPaperGuid.ToString());
                    }
                    _unitOfWork.CommitTransaction();
                    return Ok(new { Status = 1, Data = "Success" });
                }

            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                //Delete The FIles realted to the Transaction
                foreach (var fileGuid in SavedFilesGuid)
                {
                    FileLoadandSave.DeleteFile(FilePath, fileGuid);
                }
            }
            return BadRequest(new { Status = 0, Data = 2101, Message = "Error In Scheduling exam" });
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteSchedule(Guid Id)
        {
            try
            {
                string BaseFolderPath = _env.ContentRootPath;
                string FilePath = $"{BaseFolderPath}/{EXAM_PAPSERS_FILE_FOLDER}";
                var CurSchObj= _unitOfWork.ExamScheduleRepo.GetById(Id);
                if(CurSchObj!=null)
                {
                    bool Status = _unitOfWork.ExamScheduleRepo.Delete(Id);
                    if (Status)
                    {
                        _unitOfWork.SaveAction();
                        //Delete the file
                        bool DeleteStatus = FileLoadandSave.DeleteFile(FilePath, CurSchObj.ExamPaperId.ToString());
                        return Ok(new { Status = 1, Data = "Success" });
                    }
                }
               
            }
            catch(Exception ex)
            {

            }
            return BadRequest(new { Status = 0, Data = 2115, Message = "Error In Deleting schedule entry" });
        }
    }
}
