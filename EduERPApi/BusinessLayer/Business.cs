using EduERPApi.Infra;
using EduERPApi.RepoImpl;
using HOSTING=Microsoft.AspNetCore.Hosting;
namespace EduERPApi.BusinessLayer
{
    public partial class Business
    {
        private UnitOfWork _unitOfWork;
        private IConfiguration _cfg;
        ContextHelper _context;
        private HOSTING.IWebHostEnvironment _host;
        public Business(UnitOfWork uw, IConfiguration cfg, ContextHelper context, IWebHostEnvironment host)
        {
            _unitOfWork = uw;
            _context = context;
            _cfg = cfg;
            _host = host;
        }
    }
}
