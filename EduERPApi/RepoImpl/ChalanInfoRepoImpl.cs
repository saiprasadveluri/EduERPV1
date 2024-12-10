using DocumentFormat.OpenXml.InkML;
using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using Microsoft.EntityFrameworkCore.Storage;

namespace EduERPApi.RepoImpl
{
    public class ChalanInfoRepoImpl : IRepo<ChalanInfoDTO>
    {

        EduERPDbContext _context;
        
        public ChalanInfoRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public List<ChalanInfoDTO> GetByParentId(Guid ChlnId)
        {
            List <ChalanInfoDTO> infoList= new();
            var res = (from obj in _context.ChalanLineInfos
                       where obj.ChlId == ChlnId
                       select obj).ToList();
            foreach (var itm in res)
            {
                ChalanInfoDTO linfo = new ChalanInfoDTO()
                {
                    FID = itm.FeeId,
                    HN = itm.FeeHeadName,
                    TermNo = itm.TermNo,
                    TotAmt = itm.Amount,
                    Paid = itm.PaidAmt,
                    Due = itm.Amount - itm.PaidAmt,
                    DueMon = itm.DueMon
                };
                infoList.Add(linfo);
            }
            return infoList;
        }
        public Guid Add(ChalanInfoDTO item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public ChalanInfoDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, ChalanInfoDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
