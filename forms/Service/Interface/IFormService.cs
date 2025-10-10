using forms.Model.FormAuthoring;
using forms.Request;

namespace forms.Service.Interface;

public interface IFormService
{
    // IEnumerable<Form> GetAllForms();
    Form Fetch(string id);
    
    Form Create(FormRequest request);
    
    // Form UpdateForm(string id, FormRequest request);
    // void DeleteForm(string id);
}