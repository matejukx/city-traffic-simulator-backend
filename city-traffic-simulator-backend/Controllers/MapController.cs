namespace city_traffic_simulator_backend.Controllers;

using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

[Route("api/maps")]
public class MapController : ControllerBase
{
    private readonly IRepository<Map> _repository;
    private readonly ILogger<MapController> _logger;

    public MapController(IRepository<Map> repository, ILogger<MapController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetMaps()
    {
        try
        {
            var maps = _repository.GetAll();
            return Ok(maps.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("{hash}")]
    public async Task<IActionResult> GetMap(string hash)
    {
        try
        {
            var map = await _repository.GetOneAsync(map => map.Hash == hash);
            return Ok(map);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMap([FromBody] Map map)
    {
        try
        {
            await _repository.InsertAsync(map);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete("{hash}")]
    public async Task<IActionResult> DeleteMap(string hash)
    {
        try
        {
            await _repository.DeleteAsync(m => m.Hash == hash);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}