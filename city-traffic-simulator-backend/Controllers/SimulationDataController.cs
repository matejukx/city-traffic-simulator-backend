//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SimulationDataController.cs" company="automotiveMastermind">
//    © automotiveMastermind. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace city_traffic_simulator_backend.Controllers;

using AutoMapper;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Queries;
using Repository;

[ApiController]
[Route("api/simulation")]
public class SimulationDataController : ControllerBase
{
    private readonly IRepository<SimulationDataDocument> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<SimulationDataController> _logger;

    public SimulationDataController(IRepository<SimulationDataDocument> repository, ILogger<SimulationDataController> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ObjectResult> Get([FromQuery] SimulationDataQuery query)
    {
        try
        {
            var result = await _repository.GetOneAsync(d =>
                d.MapHash == query.MapHash && d.SettingsHash == query.SettingsHash);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting simulation data: {ex.Message}");
            return this.StatusCode(500, "Error while getting simulation data");
        }
       
    }

    [HttpPost]
    [ProducesResponseType(typeof(AcceptedResult), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> SaveSimulationData([FromBody] SimulationChunk chunk)
    {
        var document = _mapper.Map<SimulationDataDocument>(chunk);
        try
        {
            await _repository.UpsertAsync(document);
            return this.Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while saving simulation data: {ex.Message}");
            return this.StatusCode(500, "Error while saving simulation data");
        }
    }
}