using System.Text.Json;
using System.Text.Unicode;
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

        public void SetSession(string key, object value)
        {
            string ObjJson=JsonSerializer.Serialize(value);
           byte[] ObjBytes= System.Text.Encoding.UTF8.GetBytes(ObjJson);
            _httpContextAccessor.HttpContext.Session.Set(key, ObjBytes);
        }

        public T GetSession<T>(string key)
        {

            byte[] ObjBytes = _httpContextAccessor.HttpContext.Session.Get(key);
            if (ObjBytes != null)
            {
                string ObjJson = System.Text.Encoding.UTF8.GetString(ObjBytes);
                T Obj=(T)(JsonSerializer.Deserialize(ObjJson, typeof(T)));
                return Obj;
            }
            throw new Exception("Session Notfound exception");
        }
    }
}
