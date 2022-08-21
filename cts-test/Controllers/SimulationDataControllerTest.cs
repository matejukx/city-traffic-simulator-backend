namespace cts_test.Controllers;

using System.Threading.Tasks;
using AutoFixture;
using city_traffic_simulator_backend.Entities;
using city_traffic_simulator_backend.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

public class SimulationDataControllerTest
{
    private readonly SimulationDataControllerBuilder builder = new();

    [Fact]
    public async Task GivenSimulationDataChunk_WhenSaveSimulationDataCalled_ThenUpsertDocument()
    {
        // arrange
        var expected = builder.Fixture.Create<SimulationDataDocument>();
        var chunk = builder.Fixture.Create<SimulationChunk>();

        var controller = builder.WithMapper(expected).WithRepositoryUpsert().Build();
        
        // act 
        var result = await controller.SaveSimulationData(chunk);
        
        // assert
        Assert.Equal(result.StatusCode, StatusCodes.Status202Accepted);
        builder.Repository.Verify(mock => mock.UpsertAsync(It.Is<SimulationDataDocument>(
            d => 
                d.SettingsHash == expected.SettingsHash &&
                d.MapHash == expected.MapHash)), Times.Once);
    }
}