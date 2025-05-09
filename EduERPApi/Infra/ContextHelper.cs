namespace EduERPApi.Infra
{
    public class ContextHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext Context
        {
            get { return _httpContextAccessor.HttpContext; }
        }
    }
}
