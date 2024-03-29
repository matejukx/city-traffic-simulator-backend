namespace city_traffic_simulator_backend.Controllers;

using Entities;
using Microsoft.AspNetCore.Mvc;
using Queries;
using Repository;

[ApiController]
[Route("api/simulation")]
public class SimulationDataController : ControllerBase
{
    private readonly IRepository<SimulationDataDocument> _repository;
    private readonly ILogger<SimulationDataController> _logger;

    public SimulationDataController(IRepository<SimulationDataDocument> repository, ILogger<SimulationDataController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ObjectResult> Get([FromQuery] SimulationDataQuery query)
    {
        try
        {
            var result = await _repository.GetOneAsync(d =>
                d.MapHash == query.MapHash && d.SettingsHash == query.SettingsHash && d.RunId == query.RunId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting simulation data: {ex.Message}");
            return StatusCode(500, $"Error while getting simulation data: {ex.Message}");
        }
       
    }

    [HttpDelete]
    [ProducesResponseType(typeof(AcceptedResult), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(StatusCodeResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> Delete([FromQuery] SimulationDataQuery query)
    {
        try
        {
           var result = await _repository.DeleteAsync(d =>
                d.MapHash == query.MapHash && d.SettingsHash == query.SettingsHash && d.RunId == query.RunId);
           if (result.IsAcknowledged)
           {
               return result.DeletedCount > 0 ? Accepted() : StatusCode(404, "Simulation data not found");
           }
           return StatusCode(505, "Error while deleting simulation data");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting simulation data: {ex.Message}");
            return StatusCode(500, "Error while deleting simulation data");
        }
    }
    
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ObjectResult ListAll()
    {
        try
        {
            var result = _repository.ListAll();
            return result.Any() ? Ok(result) : StatusCode(404, "No simulation data found");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting simulation data: {ex.Message}");
            return StatusCode(500, "Error while getting simulation data");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(AcceptedResult), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> SaveSimulationData([FromBody] SimulationDataDocument document)
    {
        try
        {
            await _repository.UpsertAsync(document);
            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while saving simulation data: {ex.Message}");
            return StatusCode(500, "Error while saving simulation data");
        }
    }
}