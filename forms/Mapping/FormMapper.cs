using forms.Dto.FormAuthoring;
using forms.Enum;
using forms.Model.FormAuthoring;
using forms.Request.FormAuthoring;
using Option = forms.Model.FormAuthoring.Option;

namespace forms.Mapping;

public static class FormMapper
{
    public static Form MapToForm(FormRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return new Form
        {
            Title = request.Title,
            Description = request.Description,
            PublishedBy = request.PublishedBy,
            PublishedDate = request.PublishedDate,
            FormStatus = request.FormStatus ?? FormStatus.Draft,
            FormViewStatus = request.FormViewStatus ?? FormViewStatus.Disabled,
            QuestionLimit = request.QuestionLimit,
            AllowMultipleResponses = request.AllowMultipleResponses ?? false,
            Questions = request.Questions?.Select(MapQuestion).ToList()
        };
    }

    private static FormQuestion MapQuestion(FormQuestionRequest request)
    {
        return request.Type switch
        {
            QuestionType.ShortText => new ShortTextQuestion
            {
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Placeholder = request.Placeholder,
                Required = request.Required ?? false,
                Order = request.Order,
                MaxLength = request.MaxLength
            },

            QuestionType.LongText => new LongTextQuestion
            {
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
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Required = request.Required ?? false,
                Order = request.Order,
                MinDate = request.MinDate,
                MaxDate = request.MaxDate
            },

            QuestionType.Number => new NumberQuestion
            {
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Required = request.Required ?? false,
                Order = request.Order,
                MinValue = request.MinValue ?? int.MinValue,
                MaxValue = request.MaxValue ?? int.MaxValue
            },

            QuestionType.Select => new SelectQuestion
            {
                QuestionText = request.QuestionText ?? string.Empty,
                Description = request.Description,
                Required = request.Required ?? false,
                Order = request.Order,
                MultiSelect = request.MultiSelect ?? false,
                Options = request.Options?.Select(o => new Option
                {
                    Id = o.Id,
                    Label = o.Label,
                    Value = o.Value
                }).ToList()
            },

            _ => throw new System.Exception($"Unsupported question type: {request.Type}")
        };
    }

    public static FormDto MapToDto(Form form)
    {
        return new FormDto
        {
            Title = form.Title,
            Description = form.Description,
            PublishedBy = form.PublishedBy,
            PublishedDate = form.PublishedDate,
            FormStatus = form.FormStatus,
            FormViewStatus = form.FormViewStatus,
            QuestionLimit = form.QuestionLimit,
            AllowMultipleResponses = form.AllowMultipleResponses,
            Questions = form.Questions?.Select(MapQuestionBack).ToList()
        };
    }

    private static FormQuestionDto MapQuestionBack(FormQuestion question)
    {
        return question.Type switch
        {
            QuestionType.ShortText => question is ShortTextQuestion s
                ? new FormQuestionDto
                {
                    QuestionText = s.QuestionText ?? string.Empty,
                    Description = s.Description,
                    Placeholder = s.Placeholder,
                    Required = s.Required,
                    Order = s.Order,
                    MaxLength = s.MaxLength
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.LongText => question is LongTextQuestion l
                ? new FormQuestionDto
                {
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
                ? new FormQuestionDto
                {
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
                ? new FormQuestionDto
                {
                    QuestionText = d.QuestionText ?? string.Empty,
                    Description = d.Description,
                    Required = d.Required,
                    Order = d.Order,
                    MinDate = d.MinDate,
                    MaxDate = d.MaxDate
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.Number => question is NumberQuestion n
                ? new FormQuestionDto
                {
                    QuestionText = n.QuestionText ?? string.Empty,
                    Description = n.Description,
                    Required = n.Required,
                    Order = n.Order,
                    MinValue = n.MinValue ?? int.MinValue,
                    MaxValue = n.MaxValue ?? int.MaxValue
                }
                : throw new System.Exception("Invalid question type"),

            QuestionType.Select => question is SelectQuestion s
                ? new FormQuestionDto
                {
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
}