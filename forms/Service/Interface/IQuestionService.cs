using forms.Model.FormAuthoring;
using forms.Request.FormAuthoring;

namespace forms.Service.Interface;

public interface IQuestionService
{
    FormQuestion Create(FormQuestionRequest request);

    IEnumerable<FormQuestion> CreateMany(IEnumerable<FormQuestionRequest> requests);

    FormQuestion Update(string id, FormQuestionRequest request);

    IEnumerable<FormQuestion> UpdateMany(IEnumerable<FormQuestionRequest> requests);
}