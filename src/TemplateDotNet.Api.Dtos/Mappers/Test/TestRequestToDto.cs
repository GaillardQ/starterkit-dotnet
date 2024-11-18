using TemplateDotNet.Api.Dtos.Requests;
using TemplateDotNet.Dtos;

namespace TemplateDotNet.Api.Dtos.Mappers;

public static class TestRequestToDto
{
    public static TestDto Translate(TestRequest test)
    => new TestDto()
    {
        Name = test.Name
    };
}
