using TemplateDotNet.Dtos.Test;

namespace TemplateDotNet.Services.Test;

public interface IUserService: IService
{
    Task<List<UserDto>> GetUserList(CancellationToken cancellationToken);
    Task CreateUser(UserDto user, CancellationToken cancellationToken);
}
