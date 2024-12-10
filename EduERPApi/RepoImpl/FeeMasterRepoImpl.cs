using DocumentFormat.OpenXml.Drawing;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Infra;
using EduERPApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace EduERPApi.RepoImpl
{
    public class FeeMasterRepoImpl : IRepo<FeeMasterDTO>,IRawRepo<FeeMasterDTO,int>
    {

        EduERPDbContext _context;

        public FeeMasterRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(FeeMasterDTO item)
        {
            
            switch((FeeTypeEnum)item.AddMode)
            {
                case FeeTypeEnum.ORGANIZATION_LEVEL:
                    FeeMaster fm = new FeeMaster()
                    {
                        FeeId = Guid.NewGuid(),
                        FHeadId = item.FHeadId,
                        Amount = item.Amount,
                        AcdyearId = item.AcdyearId,
                        DueMonthNo = item.DueMonthNo,
                        DueDayNo = item.DueDayNo,
                        TermNo = item.TermNo
                    };
                    _context.Add(fm);
                break;
                case FeeTypeEnum.STREAM_LEVEL:
                    FeeMaster fm2 = new FeeMaster()
                    {
                        FeeId = Guid.NewGuid(),
                        FHeadId = item.FHeadId,
                        Amount = item.Amount,
                        AcdyearId = item.AcdyearId,
                        DueMonthNo = item.DueMonthNo,
                        DueDayNo = item.DueDayNo,
                        TermNo = item.TermNo,
                        CourseDetailId= item.CourseDetailId,
                    };
                    _context.Add(fm2);
                    break;
                case FeeTypeEnum.STUDENT_LEVEL:
                    foreach (var rec in item.MapId)
                    {
                        FeeMaster fm3 = new FeeMaster()
                        {
                            FeeId = Guid.NewGuid(),
                            FHeadId = item.FHeadId,
                            MapId = rec,
                            Amount = item.Amount,
                            AcdyearId = item.AcdyearId,
                            DueMonthNo = item.DueMonthNo,
                            DueDayNo = item.DueDayNo,
                            TermNo = item.TermNo                            

                        };
                        _context.FeeMasters.Add(fm3);
                    }
                    break;
            }
                      
            return Guid.NewGuid();//No Explicit Usage....?
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public FeeMasterDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, FeeMasterDTO item)
        {
            throw new NotImplementedException();
        }

        public int ExecuteRaw(FeeMasterDTO inp)
        {
            var enumVal = (FeeTypeEnum)inp.AddMode;
            string WhereQuery = string.Empty;
            switch (enumVal)
            {
                case FeeTypeEnum.ORGANIZATION_LEVEL:
                    WhereQuery = $"FHeadId='{inp.FHeadId}' AND TermNo={inp.TermNo} AND AcdyearId='{inp.AcdyearId}'";
                 break;
                case FeeTypeEnum.STREAM_LEVEL:
                    WhereQuery = $"FHeadId='{inp.FHeadId}' AND TermNo={inp.TermNo} AND AcdyearId='{inp.AcdyearId}' AND CourseDetailId='{inp.CourseDetailId}'";
                    break;
                case FeeTypeEnum.STUDENT_LEVEL:
                    WhereQuery = $"FHeadId='{inp.FHeadId}' AND TermNo={inp.TermNo} AND AcdyearId='{inp.AcdyearId}' AND CourseDetailId='{inp.CourseDetailId}' AND MapId='{inp.MapId}'";
                    break;
            }
            var ExistingRows=_context.FeeMasters.FromSqlRaw("SELECT * from FeeMasters WHERE "+ WhereQuery).ToList();
            if (ExistingRows != null && ExistingRows.Count > 0)
            {
                return 1;
            }
            else
                return 0;
        }
    }
}
