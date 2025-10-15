using forms.Data;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;

namespace forms.Repository.Implementations;

public class QuestionRepository : NoSQLRepository.NoSQLRepository<FormQuestion>, IQuestionRepository
{
    private readonly MongoDbContext _context;

    public QuestionRepository(MongoDbContext context) : base(context, "questions")
    {
        _context = context;
    }
}