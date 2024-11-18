namespace TemplateDotNet.Dtos;

public class TestDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string toString()
    => $"{{Id: \"{Id}\", Name: \"{Name}\", createdAt: \"{CreatedAt}\", updatedAt: \"{UpdatedAt}\"}}";
}
