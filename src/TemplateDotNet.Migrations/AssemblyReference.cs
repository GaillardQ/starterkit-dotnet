namespace TemplateDotNet.Migrations;

public static class AssemblyReference
{
    private readonly static string _name = "TemplateDotNet.Migrations";

    public static string GetName()
    => _name;
}
