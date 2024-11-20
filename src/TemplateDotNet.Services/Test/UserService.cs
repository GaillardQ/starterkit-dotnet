using TemplateDotNet.Dtos.Test;
using TemplateDotNet.Repositories.Test;
using TemplateDotNet.Utils;
using Microsoft.Extensions.Logging;

namespace TemplateDotNet.Services.Test;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository
    ) {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetUserList(CancellationToken cancellationToken)
    => await _userRepository.GetUserList(cancellationToken);

    public async Task CreateUser(UserDto user, CancellationToken cancellationToken)
    {
        await _userRepository.CreateUser(user, cancellationToken);
    }
}
