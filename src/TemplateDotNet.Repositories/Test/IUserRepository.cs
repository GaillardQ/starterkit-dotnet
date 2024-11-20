using TemplateDotNet.Domains.Test;
using TemplateDotNet.Dtos.Test;

namespace TemplateDotNet.Repositories.Test;

public interface IUserRepository : ICoreRepository<User>
{
    Task<List<UserDto>> GetUserList(CancellationToken cancellationToken);
    Task CreateUser(UserDto test, CancellationToken cancellationToken);
}