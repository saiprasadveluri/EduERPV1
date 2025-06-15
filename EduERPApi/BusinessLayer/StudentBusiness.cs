using EduERPApi.DTO;
using FileImportLibrary;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public bool StudentBulkAdd([FromForm] BulkStudentInfoDTO inp)
        {
            var SelOrgId = Guid.Parse(_context.GetSession<string>("OrgId"));
            if (inp.inpFile != null)
            {
                string StructureFileName = _cfg.GetValue<string>("EntityStructurePath:StudentInfoStructure");
                var importComponent = new EntityImport<ParsedStudentInfo>(StructureFileName);
                StudentExcelParser parser = new StudentExcelParser(inp.inpFile.OpenReadStream());
                importComponent.SetParser(parser);
                var StdInfoDTOList = importComponent.ReadContent();
                foreach (var stdInfoObj in StdInfoDTOList)
                {
                    StudentInfoDTO stuObj = new StudentInfoDTO()
                    {
                        OrgId = SelOrgId,
                        AcdYearId = inp.AcdYearId.Value,
                        StreamId = inp.StreamId.Value,
                        Email = stdInfoObj.Email,
                        Name = stdInfoObj.Name,
                        Phone = stdInfoObj.Phone,
                        Address = stdInfoObj.Address,
                        DateOfBirth = stdInfoObj.DateOfBirth,
                        DateOfJoining = stdInfoObj.DateOfJoining,
                        Status = 1,
                        Password = "MyPassword",
                        RegdNumber = stdInfoObj.RegdNumber,
                        parsedLangData = stdInfoObj.LangData
                    };
                    _unitOfWork.StudentInfoRepo.Add(stuObj);
                }
                return _unitOfWork.SaveAction();
            }
            return false;
        }
        public (Guid,bool) AddStudent(StudentInfoDTO inp)
        {
            inp.OrgId = Guid.Parse(_context.GetSession<string>("OrgId"));
            Guid NewStudentId = _unitOfWork.StudentInfoRepo.Add(inp);
            return (NewStudentId, true);
        }
    }
}
