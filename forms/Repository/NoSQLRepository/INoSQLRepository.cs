using System.Linq.Expressions;

namespace forms.Repository.NoSQLRepository;

public interface INoSQLRepository<T> where T : class
{
    IEnumerable<T> FindAll();

    T? FindById(string id);

    T FindOrThrow(string id);

    IEnumerable<T> GetByIds(IEnumerable<string> ids);
    
    T Create(T entity);

    IEnumerable<T> CreateMany(IEnumerable<T> entities);

    T Update(string id, T entity);

    IEnumerable<T> UpdateMany(IEnumerable<T> entities, Func<T, string> getId);

    void Delete(string id);

    IEnumerable<T> Find(Expression<Func<T, bool>> filter);

    long Count(Expression<Func<T, bool>> filter);
}