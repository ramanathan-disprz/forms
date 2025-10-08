using forms.Data;
using forms.Exception;
using Microsoft.EntityFrameworkCore;

namespace forms.Repository.CrudRepository;

public class CrudRepository<T> : ICrudRepository<T> where T : class
{
    private readonly ApplicationDbContext _context; // Connected to DB
    private readonly DbSet<T> _dbSet; // Table -> T

    public CrudRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IEnumerable<T> FindAll()
    {
        return _dbSet.ToList();
    }

    public T? FindById(long id)
    {
        return _dbSet.Find(id);
    }

    public T FindOrThrow(long id)
    {
        return FindById(id) ?? throw new EntityNotFoundException($"Entity with id {id} not found");
    }

    public T Create(T entity)
    {
        _dbSet.Add(entity);
        Save();
        return entity;
    }

    public T Update(T entity)
    {
        Save();
        return entity;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    private void Save()
    {
        _context.SaveChanges();
    }
}