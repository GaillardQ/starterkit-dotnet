using TemplateDotNet.Dtos;
using TemplateDotNet.Repositories;
using TemplateDotNet.Utils;
using Microsoft.Extensions.Logging;

namespace TemplateDotNet.Services;

public class TestService: ITestService
{
    private readonly ITestRepository _testRepository;

    public TestService(
        ITestRepository testRepository
    ) {
        _testRepository = testRepository;
    }

    public async Task<List<TestDto>> GetTestList(CancellationToken cancellationToken)
    => await _testRepository.GetTestList(cancellationToken);

    public async Task CreateTest(TestDto test, CancellationToken cancellationToken)
    {
        await _testRepository.CreateTest(test, cancellationToken);
    }
}
