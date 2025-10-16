using AwesomeAssertions;
using Bunit;
using DemoProject.Components.Layout;
using TestContext = Bunit.TestContext;

namespace DemoProject.Tests;

class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
}

[TestClass]
public sealed class AutocompleterIntegrationTests
{
    private IRenderedComponent<Autocompleter<Car>> _fixture = null!;
    private Autocompleter<Car> _sut = null!;
    
    [TestInitialize]
    public void Init()
    {
        var ctx = new TestContext();
        _fixture = ctx.RenderComponent<Autocompleter<Car>>(parameters =>
        {
            parameters.Add(x => x.Data,
            [
                new() { Make = "Cupra", Model = "Born" },
                new() { Make = "Peugeot", Model = "e208" },
                new() { Make = "Volkswagen", Model = "Polo" },
                new() { Make = "Mazda", Model = "3" },
                new() { Make = "Peugeot", Model = "108" },
                new() { Make = "Nissan", Model = "Pixo" },
                new() { Make = "Toyota", Model = "Rav4" },
            ]);
            parameters.Add(x => x.ItemTemplate, item => $"{item.Make} {item.Model}");
        });
        _sut = _fixture.Instance;
    }
    
    [TestMethod]
    public void Autocomplete_BasicQuery_RendersSuggestions()
    {
        // Arrange
        _sut.Query = "a";

        // Act
        _sut.Autocomplete();
        _fixture.Render();
        
        // Assert
        _fixture.FindAll("li").Count.Should().Be(5);
        _fixture.Markup.Should().Contain("Nissan Pixo");
        _fixture.Markup.Should().Contain("Cupra Born");

        // fixture.Markup.Should().Contain("qwertyuio");
        // Assert.IsTrue(fixture.Markup.Contains("bla"));
        // Assert.AreEqual(5, sut.Suggestions!.Count);
    }
}