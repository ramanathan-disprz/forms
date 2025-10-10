using System.Linq.Expressions;

namespace forms.Repository.NoSQLRepository;

public interface INoSQLRepository<T> where T : class
{
    IEnumerable<T> FindAll();
    
    T? FindById(string id);
    
    T FindOrThrow(string id);
    
    T Create(T entity);
    
    T Update(string id, T entity);
    
    void Delete(string id);
    
    IEnumerable<T> Find(Expression<Func<T, bool>> filter);
    
    long Count(Expression<Func<T, bool>> filter);
}