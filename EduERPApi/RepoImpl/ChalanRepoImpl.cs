using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.InkML;
using EduERPApi.AdhocData;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;

namespace EduERPApi.RepoImpl
{
    public class ChalanRepoImpl:IRepo<ChalanDTO>
    {
        EduERPDbContext _context;
        IDbContextTransaction _transaction;
        public ChalanRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction CurTransaction
        {
            set { _transaction = value; }
        }

       
        public List<ChalanDTO> GetByParentId(Guid MapId)
        {

            List<ChalanDTO> lst=null;
            try
            {
                lst = (from obj in _context.Chalans
                            join mobj in _context.StudentYearStreamMaps on obj.MapId equals mobj.StudentYearStreamMapId
                       join cresDetObj in _context.CourseDetails on mobj.CourseStreamId equals cresDetObj.CourseDetailId
                            join strmobj in _context.CourseSpecializations on cresDetObj.SpecializationId equals strmobj.CourseSpecializationId
                            join crsobj in _context.MainCourses on strmobj.MainCourseId equals crsobj.MainCourseId
                            join stuobj in _context.StudentInfos on mobj.StudentId equals stuobj.StudentId
                            where mobj.StudentYearStreamMapId == MapId && obj.ChalanStatus == (int)ChalanStatusEnum.Active
                            select new ChalanDTO()
                            {
                                RegdNo = stuobj.RegdNumber,
                                Name = stuobj.Name,
                                Stndardname = crsobj.CourseName + " - " + strmobj.SpecializationName,
                                ChlnNum = obj.ChlnNumber,
                                ChlnId = obj.ChlId
                            }).ToList();            

            }
            catch (Exception ex)
            {

            }
            return lst;
        }
        public Guid Add(ChalanDTO item)
        {
            Chalan ch = new Chalan();
            ch.OrgId = item.OrgId;
            ch.ChlId = Guid.NewGuid();
            ch.MapId = item.MapId;
            ch.ChlDate = DateTime.Now;
            ch.ChalanStatus =(int) ChalanStatusEnum.Active;
            var ChlnNumInfo = GetChlnNumber(item.RegdNo, item.AcdYear);

            ch.ChlnNumber = ChlnNumInfo;
            
            _context.Chalans.Add(ch);
            
            Guid NewChld = ch.ChlId;
            foreach (var lineobj in item.info)
            {
                ChalanLineInfo ln = new ChalanLineInfo();

                ln.ChlLineId = Guid.NewGuid();
                ln.ChlId = NewChld;
                ln.FeeId = lineobj.FID;
                ln.FeeHeadName = lineobj.HN;
                ln.DueMon = lineobj.DueMon;
                ln.TermNo = lineobj.TermNo;
                ln.Amount = lineobj.TotAmt;
                ln.PaidAmt = lineobj.Paid;
                _context.ChalanLineInfos.Add(ln);                
            }
            return ch.ChlId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public ChalanDTO GetById(Guid ChlnId)
        {
            var temp = (from obj in _context.Chalans
                        join mobj in _context.StudentYearStreamMaps on obj.MapId equals mobj.StudentYearStreamMapId
                        join cresDetObj in _context.CourseDetails on mobj.CourseStreamId equals cresDetObj.CourseDetailId
                        join strmobj in _context.CourseSpecializations on cresDetObj.SpecializationId equals strmobj.CourseSpecializationId
                        join crsobj in _context.MainCourses on strmobj.MainCourseId equals crsobj.MainCourseId
                        join stuobj in _context.StudentInfos on mobj.StudentId equals stuobj.StudentId
                        where obj.ChlId == ChlnId && obj.ChalanStatus == (int)ChalanStatusEnum.Active
                        select new ChalanDTO()
                        {
                            RegdNo = stuobj.RegdNumber,
                            Name = stuobj.Name,
                            Stndardname = crsobj.CourseName + " - " + strmobj.SpecializationName,
                            ChlnNum = obj.ChlnNumber,
                            ChlnId = obj.ChlId
                        }).FirstOrDefault();
            return temp;
        }

        public bool Update(Guid key, ChalanDTO item)
        {
            throw new NotImplementedException();
        }

        private string GetChlnNumber(string RegdNo,string AcdYearText)
        {
            long SeqNumber= GetNextSeqNumberBasedOnRow();
            return $"{AcdYearText}/{RegdNo}/{SeqNumber}";
        }

        private long GetNextSeqNumberBasedOnRow()
        {
            DbConnection connection=null;
            try
            {
                connection = _context.Database.GetDbConnection();
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    long anInt = 0;
                    //cmd.Transaction = _transaction.GetDbTransaction();
                    cmd.CommandText = "select top 1 ROW_NUMBER() OVER(PARTITION BY OrgId ORDER BY SeqNo) as rownum FROM Chalans order by rownum desc";
                    cmd.Connection = connection;
                    var obj = cmd.ExecuteScalar();
                    if (obj == null)
                    {
                        anInt = 1;
                    }
                    else
                        anInt = (long)obj + 1;

                    return anInt;
                }
            }
            finally
            {
                connection.Close();
            }
          
        }

        public List<StudentDataResponse> GET_STUDENT_DETAILS(StudentDataReq req)
        {
            var StuData = (from Mapobj in _context.StudentYearStreamMaps
                           join Yobj in _context.AcdYears on Mapobj.AcdYearId equals Yobj.AcdYearId
                           join stuobj in _context.StudentInfos on Mapobj.StudentId equals stuobj.StudentId
                           join cdobj in _context.CourseDetails on Mapobj.CourseStreamId equals cdobj.CourseDetailId
                           join stobj in _context.CourseSpecializations on cdobj.SpecializationId equals stobj.CourseSpecializationId
                           where Mapobj.CourseStreamId == req.CourseDetailId && Mapobj.AcdYearId == req.AcdId
                           select new StudentDataResponse() { DetailObj = Mapobj, StudentName = stuobj.Name, StreamName = stobj.SpecializationName, RegdNumber = stuobj.RegdNumber, AcdYearText = Yobj.AcdYearText }).ToList();
            return StuData;
        }

        public bool DEACTIVEATE_CHALAN(UpdateChalansStatusReq inp)
        {
            var ExitingChlns = (from cobj in _context.Chalans
                                where inp.MapList.Contains(cobj.MapId) && cobj.ChalanStatus == (int)ChalanStatusEnum.Active
                                select cobj).ToList();
            /*foreach (var chlnObj in ExitingChlns)
            {
                chlnObj.ChalanStatus = (int)ChalanStatusEnum.Inactive;
            }*/
            _context.Chalans.RemoveRange(ExitingChlns);
            _context.SaveChanges();
            return true;
        }

        public List<FeeCollectionsResponse> FEE_COLLECTION_DETAILS(FeeCollectionsRequest req)
        {
            var col = (from fobj in _context.FeeCollections
                       join chlnobj in _context.Chalans on fobj.ChlnId equals chlnobj.ChlId
                       join fcli in _context.FeeCollectionLineItems on fobj.FeeColId equals fcli.ColId
                       where chlnobj.MapId == req.MapId
                       group fcli by fcli.FeeId into g
                       select new FeeCollectionsResponse() { FID = g.Key, Amount = g.Sum(s => s.Amount) }).ToList();

            return col;
        }

        public List<FeeConcessionDTO> FEE_CONCESSION_DETAILS(FeeConcessionsRequest req)
        {
            var FeeConsData = (from FCobj in _context.FeeConcessions
                               where FCobj.MapId == req.MapId
                               select new FeeConcessionDTO()
                               {
                                   MapId = FCobj.MapId,
                                   FeeId = FCobj.FeeId,
                                   ConId = FCobj.ConId,
                                   Amount = FCobj.Amount,
                                   ConcessionType = FCobj.ConcessionType,
                                   Reason = FCobj.Reason,
                               }
                               ).ToList();
            return FeeConsData;
        }

        public List<ChalanInfoDTO> CHALAN_INFO_DETAIL(ChalanInfoRequest req)
        {
            List<ChalanInfoDTO> info = new();
            var SchoolLevel = (from feemobj in _context.FeeMasters
                               join h in _context.FeeHeadMasters on feemobj.FHeadId equals h.FeeHeadId
                               where h.FeeType == 1 && feemobj.TermNo <= req.TermNo
                               select new ChalanInfoDTO() { TermNo = feemobj.TermNo, FID = feemobj.FeeId, HN = h.FeeHeadName, TotAmt = feemobj.Amount - GetConcessionAmount(req.ConcessionList, req.StudentCourseDetailAcdYearMapId, feemobj.FeeId), Paid = req.CollectionList.Count == 0 ? 0 : req.CollectionList.Where(c => c.FID == feemobj.FeeId).Select(a => a.Amount).FirstOrDefault(), DueMon = feemobj.DueMonthNo }).ToList();
            info.AddRange(SchoolLevel);

            var ClassLevel = (from fmobj2 in _context.FeeMasters
                              join h in _context.FeeHeadMasters on fmobj2.FHeadId equals h.FeeHeadId
                              where h.FeeType == 2 && fmobj2.CourseDetailId == req.CourseDetailId && fmobj2.TermNo <= req.TermNo

                              select new ChalanInfoDTO() { TermNo = fmobj2.TermNo, FID = fmobj2.FeeId, HN = h.FeeHeadName, TotAmt = fmobj2.Amount - GetConcessionAmount(req.ConcessionList, req.StudentCourseDetailAcdYearMapId, fmobj2.FeeId), Paid = req.CollectionList.Count == 0 ? 0 : req.CollectionList.Where(c => c.FID == fmobj2.FeeId).Select(a => a.Amount).FirstOrDefault(), DueMon = fmobj2.DueMonthNo }).ToList();

            info.AddRange(ClassLevel);

            var StudentLevel = (from obj in _context.FeeMasters
                                join h in _context.FeeHeadMasters on obj.FHeadId equals h.FeeHeadId
                                where h.FeeType == 3 && obj.MapId == req.StudentCourseDetailAcdYearMapId && obj.TermNo <= req.TermNo

                                select new ChalanInfoDTO() { TermNo = obj.TermNo, FID = obj.FeeId, HN = h.FeeHeadName, TotAmt = obj.Amount - GetConcessionAmount(req.ConcessionList, req.StudentCourseDetailAcdYearMapId, obj.FeeId), Paid = req.CollectionList.Count == 0 ? 0 : req.CollectionList.Where(c => c.FID == obj.FeeId).Select(a => a.Amount).FirstOrDefault(), DueMon = obj.DueMonthNo }).ToList();

            info.AddRange(StudentLevel);
            return info;
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
