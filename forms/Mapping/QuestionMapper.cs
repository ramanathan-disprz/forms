using AutoMapper;
using forms.Dto.FormAuthoring;
using forms.Enum;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using forms.Request.FormAuthoring;
using Option = forms.Model.FormAuthoring.Option;

namespace forms.Mapping;

public class QuestionMapper
{
    private readonly IQuestionRepository _repository;

    public QuestionMapper(IQuestionRepository repository)
    {
        _repository = repository;
    }
    
    // Request --> Model
    public Question Map(QuestionRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return request.Type switch
        {
            QuestionType.ShortText => new ShortTextQuestion
            {
                FormId = request.FormId,
                Type = request.Type ?? QuestionType.ShortText,
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Placeholder = request.Placeholder,
                Required = request.Required ?? false,
                Order = request.Order,

                MaxLength = request.MaxLength
            },

            QuestionType.LongText => new LongTextQuestion
            {
                FormId = request.FormId,
                Type = request.Type ?? QuestionType.LongText,
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Placeholder = request.Placeholder,
                Required = request.Required ?? false,
                Order = request.Order,

                MinLength = request.MinLength,
                MaxLength = request.MaxLength
            },

            QuestionType.FileUpload => new FileUploadQuestion()
            {
                FormId = request.FormId,
                Type = request.Type ?? QuestionType.FileUpload,
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Placeholder = request.Placeholder,
                Required = request.Required ?? false,
                Order = request.Order,

                AllowedFileTypes = request.AllowedFileTypes,
                MaxFileSizeMB = request.MaxFileSizeMB,
                MaxTotalFileSizeMB = request.MaxTotalFileSizeMB,
                MaxFiles = request.MaxFiles
            },

            QuestionType.Date => new DateQuestion
            {
                FormId = request.FormId,
                Type = request.Type ?? QuestionType.Date,
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Placeholder = request.Placeholder,
                Required = request.Required ?? false,
                Order = request.Order,

                MinDate = request.MinDate,
                MaxDate = request.MaxDate
            },

            QuestionType.Number => new NumberQuestion
            {
                FormId = request.FormId,
                Type = request.Type ?? QuestionType.Number,
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Placeholder = request.Placeholder,
                Required = request.Required ?? false,
                Order = request.Order,

                MinValue = request.MinValue ?? int.MinValue,
                MaxValue = request.MaxValue ?? int.MaxValue
            },

            QuestionType.Select => new SelectQuestion
            {
                FormId = request.FormId,
                Type = request.Type ?? QuestionType.Select,
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Placeholder = request.Placeholder,
                Required = request.Required ?? false,
                Order = request.Order,

                MultiSelect = request.MultiSelect ?? false,
                Options = request.Options?.Select(o => new Option
                {
                    Label = o.Label,
                    Value = o.Value
                }).ToList()
            },

            _ => throw new System.Exception($"Unsupported question type: {request.Type}")
        };
    }

    public IEnumerable<Question>
        Map(IEnumerable<QuestionRequest> requests)
    {
        return requests.Select(q => Map(q));
    }
    
    // Model --> Dto
    public QuestionDto Map(Question question)
    {
        return question.Type switch
        {
            QuestionType.ShortText => question is ShortTextQuestion s
                ? new QuestionDto
                {
                    Id = s.Id,
                    Type = s.Type,
                    QuestionText = s.QuestionText ?? string.Empty,
                    Description = s.Description,
                    Placeholder = s.Placeholder,
                    Required = s.Required,
                    Order = s.Order,
                    
                    MaxLength = s.MaxLength
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.LongText => question is LongTextQuestion l
                ? new QuestionDto
                {
                    Id = l.Id,
                    Type = l.Type,
                    QuestionText = l.QuestionText ?? string.Empty,
                    Description = l.Description,
                    Placeholder = l.Placeholder,
                    Required = l.Required,
                    Order = l.Order,
                    
                    MinLength = l.MinLength,
                    MaxLength = l.MaxLength
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.FileUpload => question is FileUploadQuestion f
                ? new QuestionDto
                {
                    Id = f.Id,
                    Type = f.Type,
                    QuestionText = f.QuestionText ?? string.Empty,
                    Description = f.Description,
                    Placeholder = f.Placeholder,
                    Required = f.Required,
                    Order = f.Order,
                    
                    AllowedFileTypes = f.AllowedFileTypes,
                    MaxFileSizeMB = f.MaxFileSizeMB,
                    MaxTotalFileSizeMB = f.MaxTotalFileSizeMB,
                    MaxFiles = f.MaxFiles
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.Date => question is DateQuestion d
                ? new QuestionDto
                {
                    Id = d.Id,
                    Type = d.Type,
                    QuestionText = d.QuestionText ?? string.Empty,
                    Description = d.Description,
                    Required = d.Required,
                    Order = d.Order,
                    
                    MinDate = d.MinDate,
                    MaxDate = d.MaxDate
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.Number => question is NumberQuestion n
                ? new QuestionDto
                {
                    Id = n.Id,
                    Type = n.Type,
                    QuestionText = n.QuestionText ?? string.Empty,
                    Description = n.Description,
                    Required = n.Required,
                    Order = n.Order,
                    
                    MinValue = n.MinValue ?? int.MinValue,
                    MaxValue = n.MaxValue ?? int.MaxValue
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.Select => question is SelectQuestion s
                ? new QuestionDto
                {
                    Id = s.Id,
                    Type = s.Type,
                    QuestionText = s.QuestionText ?? string.Empty,
                    Description = s.Description,
                    Required = s.Required,
                    Order = s.Order,
                    
                    MultiSelect = s.MultiSelect ?? false,
                    Options = s.Options?.Select(o => new OptionDto()
                    {
                        Id = o.Id,
                        Label = o.Label,
                        Value = o.Value
                    }).ToList()
                }
                : throw new System.Exception("Invalid question type")
        };
    }

    public IEnumerable<QuestionDto> Map(IEnumerable<Question> questions)
    {
        return questions.Select(q => Map(q));
    }

    //Merge
    public Question Merge(Question existing, QuestionRequest request)
    {
        existing.QuestionText = request.QuestionText ?? existing.QuestionText;
        existing.Description = request.Description ?? existing.Description;
        existing.Placeholder = request.Placeholder ?? existing.Placeholder;
        existing.Required = request.Required ?? existing.Required;
        existing.Order = request.Order ?? existing.Order;

        // Type-specific updates
        switch (existing)
        {
            case ShortTextQuestion shortText when existing.Type == QuestionType.ShortText:
                shortText.MaxLength = request.MaxLength ?? shortText.MaxLength;
                break;

            case LongTextQuestion longText when existing.Type == QuestionType.LongText:
                longText.MinLength = request.MinLength ?? longText.MinLength;
                longText.MaxLength = request.MaxLength ?? longText.MaxLength;
                break;

            case FileUploadQuestion fileUpload when existing.Type == QuestionType.FileUpload:
                fileUpload.AllowedFileTypes = request.AllowedFileTypes ?? fileUpload.AllowedFileTypes;
                fileUpload.MaxFileSizeMB = request.MaxFileSizeMB ?? fileUpload.MaxFileSizeMB;
                fileUpload.MaxTotalFileSizeMB = request.MaxTotalFileSizeMB ?? fileUpload.MaxTotalFileSizeMB;
                fileUpload.MaxFiles = request.MaxFiles ?? fileUpload.MaxFiles;
                break;

            case DateQuestion date when existing.Type == QuestionType.Date:
                date.MinDate = request.MinDate ?? date.MinDate;
                date.MaxDate = request.MaxDate ?? date.MaxDate;
                break;

            case NumberQuestion number when existing.Type == QuestionType.Number:
                number.MinValue = request.MinValue ?? number.MinValue;
                number.MaxValue = request.MaxValue ?? number.MaxValue;
                break;

            case SelectQuestion select when existing.Type == QuestionType.Select:
                select.MultiSelect = request.MultiSelect ?? select.MultiSelect;
                if (request.Options != null && request.Options.Any())
                {
                    select.Options = request.Options.Select(o => new Option
                    {
                        Label = o.Label,
                        Value = o.Value
                    }).ToList();
                }

                break;
        }

        return existing;
    }

    public IEnumerable<Question> Merge(IEnumerable<QuestionRequest> requests)
    {
        // Get all IDs to update
        var idsToUpdate = requests
            .Where(q => !string.IsNullOrEmpty(q.Id))
            .Select(q => q.Id!)
            .ToList();

        // Fetch only the existing questions from the repository
        var existingQuestions = _repository.GetByIds(idsToUpdate);

        var mergedQuestions = new List<Question>();

        foreach (var qReq in requests)
        {
            if (!string.IsNullOrEmpty(qReq.Id))
            {
                var existing = existingQuestions.FirstOrDefault(q => q.Id == qReq.Id);
                if (existing != null)
                {
                    // Merge with existing question
                    mergedQuestions.Add(Merge(existing, qReq));
                }
            }
        }

        return mergedQuestions;
    }
}