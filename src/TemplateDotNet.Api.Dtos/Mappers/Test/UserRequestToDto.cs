using TemplateDotNet.Api.Dtos.Requests.Test;
using TemplateDotNet.Dtos.Test;

namespace TemplateDotNet.Api.Dtos.Mappers.Test;

public static class UserRequestToDto
{
    public static UserDto Translate(UserRequest test)
    => new UserDto()
    {
		FirstName = test.FirstName,
		LastName = test.LastName,
		Birthdate = test.Birthdate
    };
}
