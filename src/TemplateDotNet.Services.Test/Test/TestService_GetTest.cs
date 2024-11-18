using Xunit;

namespace TemplateDotNet.Services.Test;

public class AnimalService_GetTest
{
    [Fact]
    public void GetTest()
    {
        var test = true;
        Assert.True(test, "La valeur doit être 'vrai'.");
    }
}