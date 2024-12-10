using EduERPApi.Data;
using EduERPApi.DTO;
using EduERPApi.Repo;
using System.Linq;

namespace EduERPApi.RepoImpl
{
    public class OrgRepoImpl:IRepo<OrganizationDTO>
    {
        EduERPDbContext _context;
        public OrgRepoImpl(EduERPDbContext context)
        {
            _context = context;
        }
        public Guid Add(OrganizationDTO item)
        {
            Organization obj = new Organization()
            {
                OrgId = Guid.NewGuid(),                
                OrgName = item.OrgName,
                OrgAddress = item.OrgAddress,
                MobileNumber = item.MobileNumber,
                PrimaryEmail = item.PrimaryEmail,
                OrgModuleType = item.OrgModuleType,
                Status = item.Status
            };
            _context.Organizations.Add(obj);
            return obj.OrgId;
        }

        public List<OrganizationDTO> GetAll()
        {
            var temp=_context.Organizations.Join(_context.ApplicationModules, org => org.OrgModuleType, outer => outer.ModuleId, (inner, outer) =>
               new { innerObj=inner, outerObj=outer }
            ) ;
            return temp.Select(obj => new OrganizationDTO()
            {
                OrgId = obj.innerObj.OrgId,
                OrgName = obj.innerObj.OrgName,
                OrgAddress = obj.innerObj.OrgAddress,
                PrimaryEmail = obj.innerObj.PrimaryEmail,
                MobileNumber = obj.innerObj.MobileNumber,
                ModuleTypeText = obj.outerObj.ModuleName,
                OrgModuleType=obj.innerObj.OrgModuleType
            }).ToList();
        }

        public bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public OrganizationDTO GetById(Guid id)
        {
            var CurOrg=(from obj in _context.Organizations
                       where obj.OrgId == id
                       select(new OrganizationDTO()
                        {
                OrgId = obj.OrgId,
                OrgName = obj.OrgName,
                OrgAddress = obj.OrgAddress,
                PrimaryEmail = obj.PrimaryEmail,
                MobileNumber = obj.MobileNumber,
                OrgModuleType = obj.OrgModuleType
            })).FirstOrDefault();
            return CurOrg;
        }

        public bool Update(Guid key, OrganizationDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
