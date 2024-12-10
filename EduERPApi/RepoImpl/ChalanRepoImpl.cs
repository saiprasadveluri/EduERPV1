using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.InkML;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
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
            var connection = _context.Database.GetDbConnection();
            using (var cmd = connection.CreateCommand())
                {
                    long anInt = 0;
                    cmd.Transaction = _transaction.GetDbTransaction();
                    cmd.CommandText = "select top 1 ROW_NUMBER() OVER(PARTITION BY OrgId ORDER BY SeqNo) as rownum FROM Chalans order by rownum desc";
                    var obj = cmd.ExecuteScalar();
                    if (obj == null)
                    {
                        anInt = 1;
                    }
                    else          
                        anInt = (long)obj+1;

                    return anInt;
                }
          
        }

        
    }
}
