using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;

namespace EduERPApi.RepoImpl
{
    public class FeeHeadMasterRepoImpl : IRepo<FeeHeadMasterDTO>
    {
        EduERPDbContext _context;

        public FeeHeadMasterRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

        public Guid Add(FeeHeadMasterDTO item)
        {
            FeeHeadMaster feeHead = new FeeHeadMaster()
            {
                 FeeHeadId=Guid.NewGuid(),
                 FeeHeadName=item.FeeHeadName,
                 FeeType=item.FeeType,
                 OrgId=item.OrgId.Value,
                 Terms=item.Terms                 
            };
            _context.FeeHeadMasters.Add(feeHead);
            return feeHead.FeeHeadId;
        }

        public List<FeeHeadMasterDTO> GetByParentId(Guid parentId)
        {
            return _context.FeeHeadMasters.Where(fh => fh.OrgId == parentId).
                Select(fh=>new FeeHeadMasterDTO()
                {
                     FeeHeadId = fh.FeeHeadId,
                     FeeHeadName=fh.FeeHeadName,
                     FeeType= fh.FeeType,
                     Terms=fh.Terms
                }).ToList();

        }
        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public FeeHeadMasterDTO GetById(Guid id)
        {
            return _context.FeeHeadMasters.Where(fh => fh.FeeHeadId == id).Select(fh => new FeeHeadMasterDTO()
            {
                FeeHeadId = id,
                FeeHeadName=fh.FeeHeadName,
                FeeType= fh.FeeType,
                Terms=fh.Terms
            }).FirstOrDefault();
        }

        public bool Update(Guid key, FeeHeadMasterDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
