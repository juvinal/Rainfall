using Rainfall.Core.Requests;

namespace Rainfall.UnitTests;

public class GetRainfallValidatorFacts
{
    [Fact]
    public async Task Should_Pass_When_StationId_Is_Provided()
    {
        //Arrange
        var query = new GetRainfall("123", 10);
        var validator = new GetRainfallValidator();

        //Act
        var actual = await validator.ValidateAsync(query);

        //Assert
        Assert.True(actual.IsValid);
    }

    [Fact]
    public async Task Should_Be_Invalid_When_StationId_Is_Empty()
    {
        //Arrange
        var query = new GetRainfall(string.Empty, 10);
        var validator = new GetRainfallValidator();

        //Act
        var actual = await validator.ValidateAsync(query);

        //Assert
        Assert.False(actual.IsValid);
        Assert.Contains(actual.Errors, x => x.PropertyName == "StationId");
    }

    [Fact]
    public async Task Should_Be_Invalid_When_StationId_Is_Whitespace()
    {
        //Arrange
        var query = new GetRainfall(" ", 10);
        var validator = new GetRainfallValidator();

        //Act
        var actual = await validator.ValidateAsync(query);

        //Assert
        Assert.False(actual.IsValid);
        Assert.Contains(actual.Errors, x => x.PropertyName == "StationId");
    }

    [Fact]
    public async Task Should_Be_Invalid_When_Count_Is_Zero()
    {
        //Arrange
        var query = new GetRainfall("123", 0);
        var validator = new GetRainfallValidator();

        //Act
        var actual = await validator.ValidateAsync(query);

        //Assert
        Assert.False(actual.IsValid);
        Assert.Contains(actual.Errors, x => x.PropertyName == "Count");
    }

    [Fact]
    public async Task Should_Be_Invalid_When_Count_Is_Less_Than_Zero()
    {
        //Arrange
        var query = new GetRainfall("123", -10);
        var validator = new GetRainfallValidator();

        //Act
        var actual = await validator.ValidateAsync(query);

        //Assert
        Assert.False(actual.IsValid);
        Assert.Contains(actual.Errors, x => x.PropertyName == "Count");
    }

    [Fact]
    public async Task Should_Be_Invalid_When_Count_Is_More_Than_One_Hundred()
    {
        //Arrange
        var query = new GetRainfall("123", 101);
        var validator = new GetRainfallValidator();

        //Act
        var actual = await validator.ValidateAsync(query);

        //Assert
        Assert.False(actual.IsValid);
        Assert.Contains(actual.Errors, x => x.PropertyName == "Count");
    }
}
