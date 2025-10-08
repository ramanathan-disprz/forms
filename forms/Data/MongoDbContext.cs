using forms.Model;
using forms.Model.FormAuthoring;
using MongoDB.Driver;

namespace forms.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly string _questionsCollectionName = "questions";
    private readonly string _formsCollectionName = "forms";

    public IMongoCollection<BaseQuestion> Questions { get; }

    public IMongoCollection<Form> Forms { get; }

    public MongoDbContext(string connectionString, string databaseName)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("MongoDb connection string is missing.");
        if (string.IsNullOrWhiteSpace(databaseName))
            throw new InvalidOperationException("MongoDb database name is missing.");

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);

        // Initialize the base collection view
        Questions = _database.GetCollection<BaseQuestion>(_questionsCollectionName);
        Forms = _database.GetCollection<Form>(_formsCollectionName);
    }

    public MongoDbContext(IConfiguration configuration)
        : this(
            configuration.GetSection("MongoDb")["ConnectionString"]
            ?? throw new InvalidOperationException("MongoDb:ConnectionString not found."),
            configuration.GetSection("MongoDb")["DatabaseName"]
            ?? throw new InvalidOperationException("MongoDb:DatabaseName not found.")
        )
    {
    }

    // Generic accessor for any collection by name
    public IMongoCollection<T> Collection<T>(string name) => _database.GetCollection<T>(name);

    // Generic view of the questions collection for any future derived type
    public IMongoCollection<TQuestion> QuestionsAs<TQuestion>() where TQuestion : BaseQuestion =>
        _database.GetCollection<TQuestion>(_questionsCollectionName);
}