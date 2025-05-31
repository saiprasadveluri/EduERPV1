using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var SubObj = _context.OrgnizationFeatureSubscriptions.FirstOrDefault(SubObj => SubObj.SubId == key);
            if(SubObj!=null)
            {
                _context.OrgnizationFeatureSubscriptions.Remove(SubObj);
                return true;
            }
            return false;

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

        public bool DeleteBulkByOrganization(UnSubscribeFeatureData data)
        {
            var SelObj = _context.OrgnizationFeatureSubscriptions.FirstOrDefault(obj => obj.OrgId == data.OrgId && obj.FeatureId == data.FeatureId);
            if(SelObj!=null)
            {
                _context.OrgnizationFeatureSubscriptions.Remove(SelObj);
                return true;
            }
            return false;
        }
    }
}
