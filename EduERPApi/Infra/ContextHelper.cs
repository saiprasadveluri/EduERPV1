using Azure.Core;
using Microsoft.Extensions.Primitives;
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
       
        public (StringValues,bool) GetHeaderValues(string key)
        {
            bool Present = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(key, out StringValues vals);
            return (vals, Present);
        }
        public void RemoveSession(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
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
