using DocumentFormat.OpenXml.InkML;
using EduERPApi.AdhocData;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.RepoImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EduERPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChelanController : ControllerBase
    {
        UnitOfWork _unitOfWork;

        public ChelanController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("List/{Mapid}")]
        public ActionResult GetChalanList(Guid Mapid)
        {
            try
            {
                var chlnInfo = _unitOfWork.ChalanRepo.GetByParentId(Mapid);
                
                return Ok(new { Status = 1, Data = chlnInfo });

            }
            catch (Exception exp)
            {

            }
            return BadRequest(new { Status = 0, Data = 1402, Message = "Error In Getting Chalan data" });
        }
        [HttpGet("Details/{ChlnId}")]
        public ActionResult GetChalanDetails(Guid ChlnId)
        {
            try
            {
                var ChalanDTO = _unitOfWork.ChalanRepo.GetById(ChlnId);
                if (ChalanDTO != null)
                {
                    var chlnInfo = _unitOfWork.ChalanLineInfoRepo.GetByParentId(ChalanDTO.ChlnId);
                    ChalanDTO.info = chlnInfo;
                    return Ok(new { Status = 1, Data = ChalanDTO });
                }
                else
                {
                    return BadRequest(new { Status = 0, Data = 1404, Message = "Not an active Chalan" });
                }
                
            }
            catch (Exception exp)
            {

            }
            return BadRequest(new { Status = 0, Data = 1402, Message = "Error In Getting Chalan Info" });
        }
        [HttpGet]
        public ActionResult GenerateClassChalans(Guid CDetId, Guid AcdId, int TermNumber)
        {
            List<ChalanDTO> ResChalansDToList = new List<ChalanDTO>();

            bool Success = false;
            try
            {
                var SelOrgId = Guid.Parse(HttpContext.Session.GetString("OrgId"));
                
                StudentDataReq sreq = new StudentDataReq()
                {
                    CourseDetailId=CDetId,
                    AcdId=AcdId,
                    TermNo= TermNumber,
                };
                var ReqStr1=JsonConvert.SerializeObject(sreq);
                string ResStr1=_unitOfWork.AdhocLogicRepo.ExecuteCommand(OperationCodeEnum.GET_STUDENT_DETAILS, ReqStr1);
                var StuData=JsonConvert.DeserializeObject<List<StudentDataResponse>>(ResStr1);

                UpdateChalansStatusReq updateChalansStatusReq = new UpdateChalansStatusReq()
                {
                    MapList = StuData.Select(s => s.DetailObj.StudentYearStreamMapId).ToList()
                };
                var ReqStr2 = JsonConvert.SerializeObject(updateChalansStatusReq);
                string ResStr2 = _unitOfWork.AdhocLogicRepo.ExecuteCommand(OperationCodeEnum.DEACTIVEATE_CHALANS, ReqStr2);
                var obj=JsonConvert.DeserializeObject(ResStr2);
                foreach (var sobj in StuData)
                {                    
                    ChalanDTO cdto = new ChalanDTO();
                    ResChalansDToList.Add(cdto);
                    cdto.OrgId = SelOrgId;
                    cdto.MapId = sobj.DetailObj.StudentYearStreamMapId;
                    cdto.Name = sobj.StudentName;
                    cdto.Stndardname = sobj.StreamName;
                    cdto.RegdNo = sobj.RegdNumber;
                    cdto.AcdYear = sobj.AcdYearText;

                    FeeConcessionsRequest feeConcessionsRequest = new FeeConcessionsRequest()
                    {
                        MapId = sobj.DetailObj.StudentYearStreamMapId
                    };
                    var ReqStr3 = JsonConvert.SerializeObject(feeConcessionsRequest);
                    string ResStr3 = _unitOfWork.AdhocLogicRepo.ExecuteCommand(OperationCodeEnum.FEE_CONCESSION_DETAILS, ReqStr3);
                    List <FeeConcessionDTO> ConcessionData = JsonConvert.DeserializeObject<List<FeeConcessionDTO>>(ResStr3);
                   
                    
                    FeeCollectionsRequest feeCollectionsRequest = new FeeCollectionsRequest()
                    {
                        MapId = sobj.DetailObj.StudentYearStreamMapId
                    };
                    var ReqStr4 = JsonConvert.SerializeObject(feeCollectionsRequest);
                    string ResStr4 = _unitOfWork.AdhocLogicRepo.ExecuteCommand(OperationCodeEnum.FEE_COLLECTION_DETAILS, ReqStr4);
                    List<FeeCollectionsResponse> FeeCollectionsResponseData =
                        JsonConvert.DeserializeObject<List<FeeCollectionsResponse>>(ResStr4);


                    ChalanInfoRequest chalanInfoRequest = new ChalanInfoRequest()
                    {
                        CollectionList = FeeCollectionsResponseData,
                        ConcessionList = ConcessionData,
                        CourseDetailId = CDetId,
                        StudentCourseDetailAcdYearMapId = sobj.DetailObj.StudentYearStreamMapId,
                        TermNo = TermNumber
                    };
                    var ReqStr5 = JsonConvert.SerializeObject(chalanInfoRequest);
                    string ResStr5 = _unitOfWork.AdhocLogicRepo.ExecuteCommand(OperationCodeEnum.CHALAN_INFO_DETAILS, ReqStr5);
                    List<ChalanInfoDTO> ChalanInfoDTOList =
                       JsonConvert.DeserializeObject<List<ChalanInfoDTO>>(ResStr5);
                    cdto.info = ChalanInfoDTOList;
                }

                foreach(var chln in ResChalansDToList)
                {
                    try
                    {
                        _unitOfWork.BeginTransaction();
                        ((ChalanRepoImpl)(_unitOfWork.ChalanRepo)).CurTransaction = _unitOfWork.GetCurrentTransaction();
                        _unitOfWork.ChalanRepo.Add(chln);
                        _unitOfWork.SaveAction();
                        _unitOfWork.CommitTransaction();

                    }
                    catch (Exception exp)
                    {
                        _unitOfWork.RollbackTransaction();
                    }
                    
                }
                
                Success = true;
            }
            catch (Exception e)
            {
                
            }
            if (Success)
            {
                return Ok(new { Status=1,Data=true});
            }
            else
            {
                return BadRequest(new { Status = 0, Data = 1401, Message = "Error In Generating Chalans" });

            }
        }
    }
}
