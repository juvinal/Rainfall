using Moq;
using Rainfall.Core.Exceptions;
using Rainfall.Core.Requests;
using Rainfall.ReportService;
using Rainfall.ReportService.Models;

namespace Rainfall.UnitTests;

public class GetRainfallHandlerFacts
{
    private readonly Mock<IRainfallReportService> _serviceMock;

    public GetRainfallHandlerFacts()
    {
        _serviceMock = new Mock<IRainfallReportService>();
        _serviceMock
            .Setup(x => x.GetRainfallReadingsByStationAsync("123", 10, default))
            .Returns(Task.FromResult(new RainfallReport
            {
                Items = new List<ReportItem>
                {
                    new()
                    {
                        DateTime = DateTime.Now,
                        Id = "123",
                        Measure = "http://environment.data.gov.uk/flood-monitoring/id/measures/3680-rainfall-tipping_bucket_raingauge-t-15_min-mm",
                        Value = 0.2
                    }
                }
            }));
    }

    [Fact]
    public async Task Handle_Existing_Station_Returns_Success()
    {
        //Arrange
        var message = new GetRainfall("123", 10);
        var target = new GetRainfallHandler(_serviceMock.Object);

        //Act
        var actual = await target.Handle(message, CancellationToken.None);

        //Assert
        Assert.NotEmpty(actual.Readings);
    }

    [Fact]
    public async Task Handle_Non_Existing_Station_Returns_Not_Found()
    {
        //Arrange
        var message = new GetRainfall("456", 10);
        var target = new GetRainfallHandler(_serviceMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await target.Handle(message, CancellationToken.None);
        });
    }
}
