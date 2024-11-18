using TemplateDotNet.Domains;
using TemplateDotNet.Dtos;
using TemplateDotNet.Migrations;

namespace TemplateDotNet.Repositories;

public class TestRepository : CoreRepository<Test, ApiDbContext>, ITestRepository
{
    public TestRepository(
        ApiDbContext context
    ): base(context)
    {
    }

    public async Task<List<TestDto>> GetTestList(CancellationToken cancellationToken)
    {
        var list = await FindAll(cancellationToken);
        return list
            .Select(t => new TestDto()
                {
                    Name = t.Name,
                    Id = t.Id,
                }
             )
            .ToList();
    }

    public async Task CreateTest(TestDto test, CancellationToken cancellationToken)
    => await CreateAsync(new Test(test), cancellationToken);
}
