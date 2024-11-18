using Test = TemplateDotNet.Api;
namespace TemplateDotNet.Api.Dtos;


public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Api.Dtos";

    public static string GetName()
    => _name;
}
