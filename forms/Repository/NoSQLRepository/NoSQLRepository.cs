using System.Linq.Expressions;
using forms.Data;
using forms.Exception;
using MongoDB.Bson;
using MongoDB.Driver;

namespace forms.Repository.NoSQLRepository;

public class NoSQLRepository<T> : INoSQLRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;
    private readonly string _collectionName;

    public NoSQLRepository(MongoDbContext context)
    {
        var collectionName = "forms";
        _collection = context.Collection<T>(collectionName);
        _collectionName = collectionName;
    }

    public IEnumerable<T> FindAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public T? FindById(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            return null;
        }

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        return _collection.Find(filter).FirstOrDefault();
    }

    public T FindOrThrow(string id)
    {
        var entity = FindById(id);
        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity with id {id} not found in collection {_collectionName}");
        }

        return entity;
    }

    public T Create(T entity)
    {
        _collection.InsertOne(entity);
        return entity;
    }

    public T Update(string id, T entity)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            throw new ArgumentException($"Invalid ObjectId format: {id}");
        }

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        var options = new ReplaceOptions { IsUpsert = false };

        var result = _collection.ReplaceOne(filter, entity, options);

        if (result.MatchedCount == 0)
        {
            throw new EntityNotFoundException($"Entity with id {id} not found in collection {_collectionName}");
        }

        return entity;
    }

    public void Delete(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            throw new ArgumentException($"Invalid ObjectId format: {id}");
        }

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        var result = _collection.DeleteOne(filter);

        if (result.DeletedCount == 0)
        {
            throw new EntityNotFoundException($"Entity with id {id} not found in collection {_collectionName}");
        }
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
    {
        return _collection.Find(filter).ToList();
    }

    public long Count(Expression<Func<T, bool>> filter)
    {
        return _collection.CountDocuments(filter);
    }
}