namespace TemplateDotNet.Middlewares;

public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Middlewares";

    public static string GetName()
    => _name;
}
