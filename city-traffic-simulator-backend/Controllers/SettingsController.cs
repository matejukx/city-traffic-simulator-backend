namespace city_traffic_simulator_backend.Controllers;

using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

[ApiController]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly IRepository<Settings> _settingsRepository;
    private readonly ILogger<SettingsController> _logger;

    public SettingsController(IRepository<Settings>  settingsRepository, ILogger<SettingsController> logger)
    {
        _settingsRepository = settingsRepository;
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_settingsRepository.GetAll());
    }
    
    [HttpGet("{hash}")]
    public async Task<IActionResult> Get([FromRoute] string hash)
    {
        try
        {
            var result = await _settingsRepository.GetOneAsync(s => s.Hash == hash);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error while saving processed simulation data {ex.Message}");
            return StatusCode(500, "Error while saving simulation data");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Settings settings)
    {
        try
        {
            await _settingsRepository.UpsertAsync(settings);
            return Accepted();
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error while saving processed simulation data {ex.Message}");
            return StatusCode(500, "Error while saving simulation data");
        }
    }
    
    [HttpDelete("{hash}")]
    public async Task<IActionResult> Delete([FromRoute] string hash)
    {
        try
        {
            var result = await _settingsRepository.GetOneAsync(s => s.Hash == hash);
            if (result == null)
            {
                return NotFound();
            }
            
            await _settingsRepository.DeleteAsync(s => s.Hash == result.Hash);
            return Accepted();
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error while saving processed simulation data {ex.Message}");
            return StatusCode(500, "Error while saving simulation data");
        }
    }
}