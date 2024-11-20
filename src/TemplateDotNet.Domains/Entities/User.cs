using System.Transactions;
using TemplateDotNet.Dtos.Test;
using static System.Net.Mime.MediaTypeNames;

namespace TemplateDotNet.Domains.Test;

public class User : DataCoreEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
	public DateTime Birthdate { get; set; }

    public User(): base()
    {
        
    }
    
    public User(UserDto user): base()
    {
        Id = user.Id ?? Guid.NewGuid();
        FirstName = user.FirstName;
		LastName = user.LastName;
		Birthdate = user.Birthdate;
        CreatedAt = user.CreatedAt ?? DateTime.Now;
        UpdatedAt = user.UpdatedAt ?? DateTime.Now;
    }

    public UserDto ToDto()
    => new UserDto()
        { 
            Id = Id,
            FirstName = FirstName,
			LastName = LastName,
			Birthdate = Birthdate,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
    };
}
