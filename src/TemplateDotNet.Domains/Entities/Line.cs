using System.Transactions;
using TemplateDotNet.Dtos;
using static System.Net.Mime.MediaTypeNames;

namespace TemplateDotNet.Domains;

public class Line : DataCoreEntity
{
    public int Number { get; set; }
    public bool? IsError { get; set; } = false;
    public string? ErrorMessage { get; set; } = string.Empty;
    public Guid TestId { get; set; }
    public virtual Test? Test { get; set; }

    public Line()
    {
        
    }
    
    public Line(LineDto line): base()
    {
        Id = line.Id ?? Guid.NewGuid();
        Number = line.Number;
        IsError = line.IsError;
        ErrorMessage = line.ErrorMessage;
        CreatedAt = line.CreatedAt ?? DateTime.Now;
        UpdatedAt = line.UpdatedAt ?? DateTime.Now;
    }

    public LineDto ToDto()
    => new LineDto()
        { 
            Id = Id,
            Number = Number,
            IsError = IsError,
            ErrorMessage = ErrorMessage,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
    };
}
