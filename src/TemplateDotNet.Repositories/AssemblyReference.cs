namespace TemplateDotNet.Repositories;

public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Repositories";

    public static string GetName()
    => _name;
}
