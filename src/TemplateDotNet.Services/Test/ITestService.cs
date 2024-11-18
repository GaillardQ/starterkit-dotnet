using TemplateDotNet.Dtos;

namespace TemplateDotNet.Services;

public interface ITestService: IService
{
    Task<List<TestDto>> GetTestList(CancellationToken cancellationToken);
    Task CreateTest(TestDto test, CancellationToken cancellationToken);
}
