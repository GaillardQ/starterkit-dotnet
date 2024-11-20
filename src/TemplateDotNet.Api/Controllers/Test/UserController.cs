using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using TemplateDotNet.Api.Dtos.Mappers.Test;
using TemplateDotNet.Api.Dtos.Requests.Test;
using TemplateDotNet.Api.Dtos.Responses.Test;
using TemplateDotNet.Services.Test;

namespace TemplateDotNet.Api.Test;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(
        IUserService userService
    )
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReadOnlyCollection<UserResponse>>> GetTestList(CancellationToken cancellationToken)
    {
        var list = await _userService.GetUserList(cancellationToken);

        return UserDtoToResponseTranslators.Translate(list);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task CreateUser(UserRequest test, CancellationToken cancellationToken)
    {
        var userToCreate = UserRequestToDto.Translate(test);

        await _userService.CreateUser(userToCreate, cancellationToken);
    }
}
