using DocumentFormat.OpenXml.Office2010.Excel;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.RepoImpl;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        const string EXAM_PAPSERS_FILE_FOLDER = "ExamPapers";
        public string GetQuestionPaper(Guid SchId)
        {
            var SelSch = _unitOfWork.ExamScheduleRepo.GetById(SchId);
            if (SelSch != null && SelSch.ExamPaperId != Guid.Empty)
            {
                string BaseFolderPath = _host.ContentRootPath;
                string FilePath = $"{BaseFolderPath}/{EXAM_PAPSERS_FILE_FOLDER}";
                byte[] content = FileLoadandSave.ReadContent(FilePath, SelSch.ExamPaperId.ToString());
                string ContentString = Convert.ToBase64String(content);
                return ContentString;
            }
            else
            {
                throw new Exception("File Not Found");
            }
        }

        public List<ExamScheduleDTO> GetScheduleByExamId(Guid ExamId)
        {
            var Res = _unitOfWork.ExamScheduleRepo.GetByParentId(ExamId);
            return Res;
        }

        public bool AddExamSchedule(NewExamScheduleRequestDTO dto)
        {
            Guid CurQuestionPaperGuid = Guid.Empty;
            List<string> SavedFilesGuid = new List<string>();
            string BaseFolderPath = _host.ContentRootPath;
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
                    return true;
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
            return false;
        }

        public bool DeleteExamSchedule(Guid SchId)
        {
            bool DeleteStatus = true;
            string BaseFolderPath = _host.ContentRootPath;
            string FilePath = $"{BaseFolderPath}/{EXAM_PAPSERS_FILE_FOLDER}";
            try
            {
                _unitOfWork.BeginTransaction();
                var CurSchObj = _unitOfWork.ExamScheduleRepo.GetById(SchId);
                if (CurSchObj != null)
                {
                    bool Status = _unitOfWork.ExamScheduleRepo.Delete(SchId);
                    if (Status)
                    {
                        _unitOfWork.SaveAction();
                        //Delete the file
                        DeleteStatus = FileLoadandSave.DeleteFile(FilePath, CurSchObj.ExamPaperId.ToString());
                        if(DeleteStatus)
                        {
                            _unitOfWork.CommitTransaction();
                            return true;
                        }
                        else
                        {
                            _unitOfWork.RollbackTransaction();
                        }
                    }
                }
            }
            catch(Exception exp)
            {
                _unitOfWork.RollbackTransaction();
            }
            return false;
        }
    }
}