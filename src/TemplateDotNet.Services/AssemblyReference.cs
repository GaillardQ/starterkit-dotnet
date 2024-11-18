namespace TemplateDotNet.Services;

public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Services";

    public static string GetName()
    => _name;
}
