using System.Linq.Expressions;

namespace StudentPersonalAccount.Interfaces
{
    public interface IRepository<T> where T : IGuidKey
    {
        T Get(Guid guid);
        T Add(T model);
        IEnumerable<T> AddRange(IEnumerable<T> models);
        bool Update(T models);
        bool UpdateRange(IEnumerable<T> models);
        bool Remove(T model);
        bool Remove(Guid Guid);
        bool RemoveRange(IEnumerable<T> models);
        bool RemoveRange(IEnumerable<Guid> guids);
        IQueryable<T> GetListQuery();
        IQueryable<T> GetListQueryWithDeleted();
        List<T> GetList();
        IEnumerable<T> GetListWithDeleted();
        bool Any(Expression<Func<T, bool>> func);
        T FirstOrDefault(Expression<Func<T, bool>> func);
    }
}
