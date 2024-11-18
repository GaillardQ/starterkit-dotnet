using TemplateDotNet.Domains;
using TemplateDotNet.Dtos;

namespace TemplateDotNet.Repositories;

public interface ITestRepository : ICoreRepository<Test>
{
    Task<List<TestDto>> GetTestList(CancellationToken cancellationToken);
    Task CreateTest(TestDto test, CancellationToken cancellationToken);
}