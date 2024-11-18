namespace TemplateDotNet.Api;

public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Api";

    public static string GetName()
    => _name;
}
