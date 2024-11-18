namespace TemplateDotNet.Domains;

public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Domains";

    public static string GetName()
    => _name;
}
