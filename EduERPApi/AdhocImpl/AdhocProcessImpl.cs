using EduERPApi.AdhocData;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.Repo;
using Newtonsoft.Json;
using System.Linq;

namespace EduERPApi.AdhocImpl
{
    public class AdhocProcessImpl : IAdhocLogicRepo
    {
        EduERPDbContext context;

        public AdhocProcessImpl(EduERPDbContext context)
        {
            this.context = context;
        }

       

        public string ExecuteCommand(OperationCodeEnum opt, string inpStr)
        {
            switch (opt)
            {
                case OperationCodeEnum.DEACTIVEATE_CHALANS:
                    var inp = JsonConvert.DeserializeObject<UpdateChalansStatusReq>(inpStr);
                    var ExitingChlns = (from cobj in context.Chalans
                                        where inp.MapList.Contains(cobj.MapId) && cobj.ChalanStatus == (int)ChalanStatusEnum.Active
                                        select cobj).ToList();
                    foreach (var chlnObj in ExitingChlns)
                    {
                        chlnObj.ChalanStatus = (int)ChalanStatusEnum.Inactive;
                    }
                    context.SaveChanges();
                    JsonConvert.SerializeObject(new { Status = true, Data = true });
                    break;

                case OperationCodeEnum.FEE_COLLECTION_DETAILS:
                    var inpObj1 = JsonConvert.DeserializeObject<FeeCollectionsRequest>(inpStr);
                    var col = (from fobj in context.FeeCollections
                               join chlnobj in context.Chalans on fobj.ChlnId equals chlnobj.ChlId
                               join fcli in context.FeeCollectionLineItems on fobj.FeeColId equals fcli.ColId
                               where chlnobj.MapId == inpObj1.MapId
                               group fcli by fcli.FeeId into g
                               select new FeeCollectionsResponse() { FID = g.Key, Amount = g.Sum(s => s.Amount) }).ToList();
                    return JsonConvert.SerializeObject(col);
                    break;

                case OperationCodeEnum.GET_STUDENT_DETAILS:
                    var inpobj2 = JsonConvert.DeserializeObject<StudentDataReq>(inpStr);
                    var StuData = (from Mapobj in context.StudentYearStreamMaps
                                   join Yobj in context.AcdYears on Mapobj.AcdYearId equals Yobj.AcdYearId
                                   join stuobj in context.StudentInfos on Mapobj.StudentId equals stuobj.StudentId
                                   join cdobj in context.CourseDetails on Mapobj.CourseStreamId equals cdobj.CourseDetailId
                                   join stobj in context.CourseSpecializations on cdobj.SpecializationId equals stobj.CourseSpecializationId
                                   where Mapobj.CourseStreamId == inpobj2.CourseDetailId && Mapobj.AcdYearId == inpobj2.AcdId
                                   select new StudentDataResponse() { DetailObj = Mapobj, StudentName = stuobj.Name, StreamName = stobj.SpecializationName, RegdNumber = stuobj.RegdNumber, AcdYearText = Yobj.AcdYearText }).ToList();
                    return JsonConvert.SerializeObject(StuData);
                    break;
                    case OperationCodeEnum.FEE_CONCESSION_DETAILS:
                    var inpobj3 = JsonConvert.DeserializeObject<FeeConcessionsRequest>(inpStr);
                    var FeeConsData = (from FCobj in context.FeeConcessions
                                       where FCobj.MapId == inpobj3.MapId
                                       select new FeeConcessionDTO() { 
                                        MapId= FCobj.MapId,
                                        FeeId=FCobj.FeeId,
                                        ConId=FCobj.ConId,
                                        Amount= FCobj.Amount,
                                        ConcessionType=FCobj.ConcessionType,
                                        Reason=FCobj.Reason,
                                       }
                                       ).ToList();
                    return JsonConvert.SerializeObject(FeeConsData);
                    break;

                case OperationCodeEnum.CHALAN_INFO_DETAILS:
                    var inpobj4 = JsonConvert.DeserializeObject<ChalanInfoRequest>(inpStr);
                    List<ChalanInfoDTO> info = new ();
                    var SchoolLevel = (from feemobj in context.FeeMasters
                                       join h in context.FeeHeadMasters on feemobj.FHeadId equals h.FeeHeadId
                                       where h.FeeType == 1 && feemobj.TermNo <= inpobj4.TermNo
                                       select new ChalanInfoDTO() { TermNo = feemobj.TermNo, FID = feemobj.FeeId, HN = h.FeeHeadName, TotAmt = feemobj.Amount - GetConcessionAmount(inpobj4.ConcessionList, inpobj4.StudentCourseDetailAcdYearMapId, feemobj.FeeId), Paid = inpobj4.CollectionList.Count==0?0:inpobj4.CollectionList.Where(c => c.FID == feemobj.FeeId).Select(a => a.Amount).FirstOrDefault(), DueMon = feemobj.DueMonthNo }).ToList();
                    info.AddRange(SchoolLevel);

                    var ClassLevel = (from fmobj2 in context.FeeMasters
                                      join h in context.FeeHeadMasters on fmobj2.FHeadId equals h.FeeHeadId
                                      where h.FeeType == 2 && fmobj2.CourseDetailId == inpobj4.CourseDetailId && fmobj2.TermNo <= inpobj4.TermNo

                                      select new ChalanInfoDTO() { TermNo = fmobj2.TermNo, FID = fmobj2.FeeId, HN = h.FeeHeadName, TotAmt = fmobj2.Amount - GetConcessionAmount(inpobj4.ConcessionList, inpobj4.StudentCourseDetailAcdYearMapId, fmobj2.FeeId), Paid = inpobj4.CollectionList.Count == 0 ? 0 : inpobj4.CollectionList.Where(c => c.FID == fmobj2.FeeId).Select(a => a.Amount).FirstOrDefault(), DueMon = fmobj2.DueMonthNo }).ToList();

                    info.AddRange(ClassLevel);

                    var StudentLevel = (from obj in context.FeeMasters
                                        join h in context.FeeHeadMasters on obj.FHeadId equals h.FeeHeadId
                                        where h.FeeType == 3 && obj.MapId == inpobj4.StudentCourseDetailAcdYearMapId && obj.TermNo <= inpobj4.TermNo

                                        select new ChalanInfoDTO() { TermNo = obj.TermNo, FID = obj.FeeId, HN = h.FeeHeadName, TotAmt = obj.Amount - GetConcessionAmount(inpobj4.ConcessionList, inpobj4.StudentCourseDetailAcdYearMapId, obj.FeeId), Paid = inpobj4.CollectionList.Count == 0 ? 0 : inpobj4.CollectionList.Where(c => c.FID == obj.FeeId).Select(a => a.Amount).FirstOrDefault(), DueMon = obj.DueMonthNo }).ToList();

                    info.AddRange(StudentLevel);
                    return JsonConvert.SerializeObject(info);
                    break;

            }
            return String.Empty;
        }

        private static double GetConcessionAmount(List<FeeConcessionDTO> cons, Guid MapId, Guid FeeId)
        {
            double res = (from obj in cons
                          where obj.MapId == MapId && obj.FeeId == FeeId
                          select obj.Amount).FirstOrDefault();
            return res;
        }
    }
}
