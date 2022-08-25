namespace city_traffic_simulator_backend.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Repository;

[ApiController]
[Route("api/processed")]
public class ProcessedDataController : ControllerBase
{
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly ILogger<ProcessedDataController> _logger;

    public ProcessedDataController(MongoContext context, ILogger<ProcessedDataController> logger)
    {
        _logger = logger;
        _collection = context.Database.GetCollection<BsonDocument>("processedData");
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> GetProcessedData([FromRoute] string id)
    {
        try
        {
            var document = _collection.Find(doc => doc["_id"] == new ObjectId(id)).First().ToJson();
            return this.Ok(document);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting processed simulation data {ex.Message}");
        }

        return new ObjectResult(this.NotFound());
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CreatedAtActionResult), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> SaveProcessedData([FromBody] JsonElement content)
    {
        try
        {
            var json = content.GetRawText();
            var document = BsonSerializer.Deserialize<BsonDocument>(json);

            await _collection.InsertOneAsync(document);
            return CreatedAtAction(nameof(SaveProcessedData),
                new { id = document["_id"].ToString()});
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while saving processed simulation data {ex.Message}");
            return this.StatusCode(500, "Error while saving simulation data");
        }
    }
}