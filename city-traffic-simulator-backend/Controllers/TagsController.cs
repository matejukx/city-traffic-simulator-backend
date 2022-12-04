namespace city_traffic_simulator_backend.Controllers;

using Entities;
using Microsoft.AspNetCore.Mvc;
using Queries;
using Repository;

[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly IRepository<TagDocument> _repository;

    public TagsController(IRepository<TagDocument> repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(_repository.GetAll().ToList());
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult Post([FromBody] TagDocument tag)
    {
        _repository.InsertAsync(tag);
        return Accepted(tag);
    }
    
    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> DeleteProcessedData([FromQuery] TagQuery query)
    {
        try
        {
            await _repository.DeleteAsync(t => t.Name == query.Name);
            return Accepted();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error while deleting simulation data {ex.Message}");
        }
    }
}