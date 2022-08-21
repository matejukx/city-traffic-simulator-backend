namespace cts_test.Controllers;

using System;
using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using city_traffic_simulator_backend.Controllers;
using city_traffic_simulator_backend.Entities;
using city_traffic_simulator_backend.Entities.Dto;
using city_traffic_simulator_backend.Repository;
using Microsoft.Extensions.Logging;
using Moq;

public class SimulationDataControllerBuilder
{
    public Fixture Fixture = new();
    public Mock<IRepository<SimulationDataDocument>> Repository { get; set; } = new();
    public Mock<ILogger<SimulationDataController>> Logger { get; set; } = new();
    public Mock<IMapper> Mapper { get; set; } = new();

    public SimulationDataControllerBuilder WithMapper(SimulationDataDocument result)
    {
        Mapper.Setup(mock => 
            mock.Map<SimulationDataDocument>(It.IsAny<SimulationChunk>()))
            .Returns(result);
        return this;
    }

    public SimulationDataControllerBuilder WithRepositoryUpsert(Exception? exception = null)
    {
        if (exception is not null)
        {
            Repository.Setup(mock =>
                    mock.UpsertAsync(It.IsAny<SimulationDataDocument>()))
                .ThrowsAsync(exception);
        }

        return this;
    }

    public SimulationDataControllerBuilder WithRepositoryGetOneResult(SimulationDataDocument result)
    {
        Repository.Setup(mock => 
                mock.GetOneAsync(It.IsAny<Expression<Func<SimulationDataDocument, bool>>>()))
            .ReturnsAsync(result);
        return this;
    }
    
    public SimulationDataController Build()
    {
        return new SimulationDataController(this.Repository.Object, this.Logger.Object, this.Mapper.Object);
    }
}