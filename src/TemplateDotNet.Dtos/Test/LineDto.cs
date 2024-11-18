namespace TemplateDotNet.Dtos;

public class LineDto
{
    public Guid? Id { get; set; }
    public int Number { get; set; }
    public bool? IsError { get; set; } = false;
    public string? ErrorMessage { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string toString()
    => $"{{Id: \"{Id}\", Number: \"{Number}\", IsError: \"{IsError}\", ErrorMessage: \"{ErrorMessage}\", createdAt: \"{CreatedAt}\", updatedAt: \"{UpdatedAt}\"}}";
}
