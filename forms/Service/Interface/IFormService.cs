using forms.Model.FormAuthoring;
using forms.Request;
using forms.Request.FormAuthoring;

namespace forms.Service.Interface;

public interface IFormService
{
    // IEnumerable<Form> GetAllForms();
    Form Fetch(string id);

    Form Create(FormRequest request);

    Form Update(string id, FormRequest request);
    
    void Delete(string id);
}