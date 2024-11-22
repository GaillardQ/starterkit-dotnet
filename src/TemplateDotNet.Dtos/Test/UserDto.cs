namespace TemplateDotNet.Dtos.Test;

public class UserDto
{
    public Guid? Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
	public DateTime Birthdate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string toString()
    => $"{{Id: \"{Id}\", FirstName: \"{FirstName}\", LastName: \"{LastName}\", Birthdate: \"{Birthdate}\", createdAt: \"{CreatedAt}\", updatedAt: \"{UpdatedAt}\"}}";
}
