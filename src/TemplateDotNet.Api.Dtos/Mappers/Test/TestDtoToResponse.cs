using System.Collections.ObjectModel;
using TemplateDotNet.Api.Dtos.Responses;
using TemplateDotNet.Dtos;

namespace TemplateDotNet.Api.Dtos.Mappers;

public static class TestDtoToResponseTranslators
{
    public static TestResponse Translate(TestDto test)
    => new TestResponse()
    {
        Id = test.Id ?? Guid.Empty,
        Name = test.Name
    };

    public static ReadOnlyCollection<TestResponse> Translate(List<TestDto> list)
    => list
        .Select(a => Translate(a))
        .ToList()
        .AsReadOnly();

}
