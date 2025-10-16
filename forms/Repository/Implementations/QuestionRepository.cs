using forms.Data;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace forms.Repository.Implementations;

public class QuestionRepository : NoSQLRepository.NoSQLRepository<Question>, IQuestionRepository
{
    private readonly MongoDbContext _context;
    private readonly ILogger<QuestionRepository> _logger;

    public QuestionRepository(MongoDbContext context, ILogger<QuestionRepository> logger)
        : base(context, "questions")
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<Question> IndexByFormId(string formId)
    {
        if (string.IsNullOrEmpty(formId))
            throw new ArgumentException("FormId cannot be null or empty", nameof(formId));

        var filter = Builders<Question>.Filter.Eq(q => q.FormId, formId);
        var questions = _collection.Find(filter).ToList();

        _logger.LogInformation($"Found {questions.Count} questions for form with id {formId}");
        return questions;
    }

    public void DeleteByFormId(string formId)
    {
        var filter = Builders<Question>.Filter.Eq(q => q.FormId, formId);
        var result = _collection.DeleteMany(filter);
        _logger.LogInformation($"Deleted {result.DeletedCount} records from the form with id {formId}");
    }
}