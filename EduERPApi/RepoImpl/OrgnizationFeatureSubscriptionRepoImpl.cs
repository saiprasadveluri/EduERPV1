using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using Microsoft.AspNetCore.Mvc;

namespace EduERPApi.RepoImpl
{
    public class OrgnizationFeatureSubscriptionRepoImpl : IRepo<OrgnizationFeatureSubscriptionDTO>
    {
        EduERPDbContext _context;
        public OrgnizationFeatureSubscriptionRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }

       
        public Guid Add(OrgnizationFeatureSubscriptionDTO item)
        {
            
            OrgnizationFeatureSubscription orgSub = new OrgnizationFeatureSubscription()
            {
                FeatureId = item.FeatureId,
                OrgId = item.OrgId,
                Status = 1,
                SubId = Guid.NewGuid()
            };
            _context.OrgnizationFeatureSubscriptions.Add(orgSub);
            return orgSub.SubId;
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public OrgnizationFeatureSubscriptionDTO GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid key, OrgnizationFeatureSubscriptionDTO item)
        {
            throw new NotImplementedException();
        }

        public List<OrgnizationFeatureSubscriptionDTO> GetByParentId(Guid OrgId)
        {
            return (from obj in _context.OrgnizationFeatureSubscriptions
                       where obj.OrgId == OrgId
                       select new OrgnizationFeatureSubscriptionDTO
                       {
                           FeatureId = obj.FeatureId,
                           OrgId = OrgId,
                           Status = obj.Status,
                           SubId = obj.SubId,
                       }).ToList();
        }
    }
}
