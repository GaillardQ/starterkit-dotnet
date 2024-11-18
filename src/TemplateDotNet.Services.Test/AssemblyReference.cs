namespace TemplateDotNet.Services.Test;

public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Services.Test";

    public static string GetName()
    => _name;
}
