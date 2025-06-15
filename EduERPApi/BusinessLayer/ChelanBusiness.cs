using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using EduERPApi.AdhocData;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.RepoImpl;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        public List<ChalanDTO> GetChalanList(Guid Mapid)
        {
            var ChalanRepo = _unitOfWork.ChalanRepo;
            return ChalanRepo.GetByParentId(Mapid);
        }

        public List<ChalanInfoDTO> GetChalanInfoByParentId(Guid id)
        {
            return _unitOfWork.ChalanLineInfoRepo.GetByParentId(id);
        }
        public ChalanDTO GetChalanDetails(Guid ChlnId)
        {
            var ChalanRepo = _unitOfWork.ChalanRepo;
            var ChalanDTO = ChalanRepo.GetById(ChlnId);
            if (ChalanDTO != null)
            {
                var chlnInfo = _unitOfWork.ChalanLineInfoRepo.GetByParentId(ChalanDTO.ChlnId);
                ChalanDTO.info = chlnInfo;
                return ChalanDTO;
            }
            else
            {
                return null;
            }
        }

        public bool GenerateClassChalans(Guid CourseDetId, Guid AcdId, int TermNumber)
        {
            var ChalanRepo = _unitOfWork.ChalanRepo;
            List<ChalanDTO> ResChalansDToList = new List<ChalanDTO>();
            bool Success = false;
            var SelOrgId = Guid.Parse(_context.GetSession<string>("OrgId"));
            StudentDataReq sreq = new StudentDataReq()
            {
                CourseDetailId = CourseDetId,
                AcdId = AcdId,
                TermNo = TermNumber,
            };
           var StuData = ChalanRepo.GET_STUDENT_DETAILS(sreq);
            UpdateChalansStatusReq updateChalansStatusReq = new UpdateChalansStatusReq()
            {
                MapList = StuData.Select(s => s.DetailObj.StudentYearStreamMapId).ToList()
            };
            bool DeactivateRes=ChalanRepo.DEACTIVEATE_CHALAN(updateChalansStatusReq);
            
            foreach (var sobj in StuData)
            {
                ChalanDTO cdto = new ChalanDTO();
                ResChalansDToList.Clear();
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
                var ConcessionData = ChalanRepo.FEE_CONCESSION_DETAILS(feeConcessionsRequest);
                FeeCollectionsRequest feeCollectionsRequest = new FeeCollectionsRequest()
                {
                    MapId = sobj.DetailObj.StudentYearStreamMapId
                };

               var FeeCollectionsResponseData = ChalanRepo.FEE_COLLECTION_DETAILS(feeCollectionsRequest);
                ChalanInfoRequest chalanInfoRequest = new ChalanInfoRequest()
                {
                    CollectionList = FeeCollectionsResponseData,
                    ConcessionList = ConcessionData,
                    CourseDetailId = CourseDetId,
                    StudentCourseDetailAcdYearMapId = sobj.DetailObj.StudentYearStreamMapId,
                    TermNo = TermNumber
                };
                List<ChalanInfoDTO> ChalanInfoDTOList=ChalanRepo.CHALAN_INFO_DETAIL(chalanInfoRequest);
                cdto.info = ChalanInfoDTOList;
                foreach (var chln in ResChalansDToList)
                {
                    try
                    {
                        //_unitOfWork.BeginTransaction();
                        //((ChalanRepoImpl)(_unitOfWork.ChalanRepo)).CurTransaction = _unitOfWork.GetCurrentTransaction();
                        _unitOfWork.ChalanRepo.Add(chln);
                        _unitOfWork.SaveAction();
                        //_unitOfWork.CommitTransaction();

                    }
                    catch (Exception exp)
                    {
                        _unitOfWork.RollbackTransaction();
                    }

                }
            }
            return true;
        }       

    }
}
