using forms.Dto.FormAuthoring;
using forms.Model.FormAuthoring;
using forms.Request;
using forms.Request.FormAuthoring;

namespace forms.Service.Interface;

public interface IFormService
{
    IEnumerable<Form> Index();
    
    Form Fetch(string id);
    
    FormWithQuestions FetchWithQuestions(string id);
    
    Form Create(FormRequest request);

    Form Update(string id, FormRequest request);
    
    void Delete(string id);
}