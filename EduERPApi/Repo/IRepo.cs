using EduERPApi.DTO;

namespace EduERPApi.Repo
{
    public interface IRepo<T> where T : class
    {
        List<T> GetAll()
        {
            return null;
        }
        T GetById(Guid id);
        Guid Add(T item);
        bool Update(Guid key, T item);
        bool Delete(Guid key);
        List<T> GetByParentId(Guid parentId)
        {
            return null;
        }
              
    }

    public interface IRawRepo<L,M>
    {
        M ExecuteRaw(L inp)
        {
            return default(M);
        }
    }

    public interface IExecuteRawSql
    {
        Object ExecuteSqlRaw(string sql);
    }
}
