using EduERPApi.Infra;
using EduERPApi.RepoImpl;

namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        private UnitOfWork _unitOfWork;
        private IConfiguration _cfg;
        ContextHelper _context;
        public Business(UnitOfWork uw, IConfiguration cfg, ContextHelper context)
        {
            _unitOfWork = uw;
            _context = context;
            _cfg = cfg;
        }
    }
}
