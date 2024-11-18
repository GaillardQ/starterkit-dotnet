using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using TemplateDotNet.Api.Dtos.Mappers;
using TemplateDotNet.Api.Dtos.Requests;
using TemplateDotNet.Api.Dtos.Responses;
using TemplateDotNet.Services;

namespace TemplateDotNet.Api;

[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
[Route("test")]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(
        ITestService testService
    )
    {
        _testService = testService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ReadOnlyCollection<TestResponse>>> GetTestList(CancellationToken cancellationToken)
    {
        var list = await _testService.GetTestList(cancellationToken);

        return TestDtoToResponseTranslators.Translate(list);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task CreateAnimal(TestRequest test, CancellationToken cancellationToken)
    {
        var testToCreate = TestRequestToDto.Translate(test);

        await _testService.CreateTest(testToCreate, cancellationToken);
    }
}
