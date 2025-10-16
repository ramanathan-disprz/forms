using forms.Model.FormAuthoring;
using forms.Request.FormAuthoring;

namespace forms.Service.Interface;

public interface IQuestionService
{
    IEnumerable<Question> IndexByFormId(string formId);

    Question Fetch(string id);

    Question Create(QuestionRequest request);

    IEnumerable<Question> CreateMany(IEnumerable<QuestionRequest> requests);

    Question Update(string id, QuestionRequest request);

    IEnumerable<Question> UpdateMany(IEnumerable<QuestionRequest> requests);
    
    void Delete(string id);

    void DeleteByFormId(string formId);
}