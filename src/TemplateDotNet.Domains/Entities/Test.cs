using TemplateDotNet.Dtos;

namespace TemplateDotNet.Domains;

public class Test : DataCoreEntity
{
    public string Name { get; set; } = string.Empty;
    public virtual List<Line>? Lines { get; set; }

    public Test()
    {
        
    }
    
    public Test(TestDto test): base()
    { 
        Id = test.Id ?? Guid.NewGuid();
        Name = test.Name;
        CreatedAt = test.CreatedAt ?? DateTime.Now;
        UpdatedAt = test.UpdatedAt ?? DateTime.Now;
    }

    public TestDto ToDto()
    => new TestDto()
        { 
            Id = Id,
            Name = Name,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
        };
}
