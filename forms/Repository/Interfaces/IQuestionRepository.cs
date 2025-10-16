using forms.Model.FormAuthoring;

namespace forms.Repository.Interfaces;

public interface IQuestionRepository : NoSQLRepository.INoSQLRepository<Question>
{ 
    IEnumerable<Question> IndexByFormId(String formId);
    
    void DeleteByFormId(string formId);
}