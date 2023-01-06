using StudentPersonalAccount.EF;
using StudentPersonalAccount.Interfaces;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace StudentPersonalAccount.Services;

public class Repository<T> : IRepository<T> where T : class, IGuidKey
{
    private readonly PersonalAccountContext _db;
    private readonly ILogger<IRepository<T>> _logger;

    public Repository(PersonalAccountContext db,
        ILogger<IRepository<T>> logger)
    {
        _db = db;
        _logger = logger;
    }

    public virtual T Add(T model)
    {
        _db.Add(model);
        _db.SaveChanges();

        return model;
    }

    public virtual IEnumerable<T> AddRange(IEnumerable<T> models)
    {
        try
        {
            _db.AddRange(models);
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при добавлении сущностей: {ex}");
        }
        return models;
    }

    public virtual bool Update(T model)
    {
        try
        {
            _db.Update(model);
            _db.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при обновлении сущности: {ex}");
            return false;
        }
    }

    public virtual bool UpdateRange(IEnumerable<T> models)
    {
        try
        {
            _db.UpdateRange(models);
            _db.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при обновлении сущностей: {ex}");
            return false;
        }
    }

    public virtual bool Remove(T model)
    {
        if (model == null) return true;

        try
        {
            _db.Remove(model);
            _db.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при удалении сущности: {ex}");
            return false;
        }
    }

    public virtual bool Remove(Guid Guid)
    {
        try
        {
            var model = _db.Set<T>().AsNoTracking().FirstOrDefault(p => p.Guid == Guid);
            return Remove(model);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при удалении сущности: {ex}");
            return false;
        }
    }

    public virtual bool RemoveRange(IEnumerable<T> models)
    {
        try
        {
            _db.RemoveRange(models);
            _db.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при удалении сущностей: {ex}");
            return false;
        }
    }

    public bool RemoveRange(IEnumerable<Guid> guids)
    {
        if (guids == null || guids.Count() == 0) return true;
        return RemoveRange(_db.Set<T>().Where(p => guids.Contains(p.Guid)));
    }

    public virtual IQueryable<T> GetListQuery()
    {
        return _db.Set<T>().AsNoTracking().AsQueryable();
    }

    public virtual IQueryable<T> GetListQueryWithDeleted()
    {
        return _db.Set<T>().AsNoTracking().AsQueryable();
    }

    public virtual List<T> GetList()
    {
        return _db.Set<T>().AsNoTracking().ToList();
    }

    public virtual IEnumerable<T> GetListWithDeleted()
    {
        return _db.Set<T>().AsNoTracking().AsQueryable();
    }

    public virtual bool Any(Expression<Func<T, bool>> func)
    {
        return GetListQuery().Any(func);
    }

    public virtual T FirstOrDefault(Expression<Func<T, bool>> func)
    {
        return GetListQuery().FirstOrDefault(func);
    }

    public T Get(Guid guid)
    {
        return GetListQuery().FirstOrDefault(p => p.Guid == guid);
    }
}
