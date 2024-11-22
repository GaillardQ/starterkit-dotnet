using System.Collections.ObjectModel;
using TemplateDotNet.Api.Dtos.Responses.Test;
using TemplateDotNet.Dtos.Test;

namespace TemplateDotNet.Api.Dtos.Mappers.Test;

public static class UserDtoToResponseTranslators
{
    public static UserResponse Translate(UserDto user)
    => new UserResponse()
    {
        Id = user.Id ?? Guid.Empty,
        FirstName = user.FirstName,
		LastName = user.LastName,
		Birthdate = user.Birthdate,
		CreatedAt = user.CreatedAt,
		UpdatedAt = user.UpdatedAt
    };

    public static ReadOnlyCollection<UserResponse> Translate(List<UserDto> list)
    => list
        .Select(a => Translate(a))
        .ToList()
        .AsReadOnly();

}
