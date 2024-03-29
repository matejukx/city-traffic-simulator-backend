namespace city_traffic_simulator_backend.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Queries;
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
    [Route("find")]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> GetProcessedData([FromQuery] ProcessedDataQuery processedDataQuery)
    {
        try
        {
            var documents = await _collection.FindAsync(doc =>
                doc["MapHash"] == processedDataQuery.MapHash
                && doc["SettingsHash"] == processedDataQuery.SettingsHash
                && doc["RunId"] == processedDataQuery.RunId);
            var document = documents.FirstOrDefault();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            var json = document.ToJson(jsonWriterSettings);
            json = json.Replace("\\", "");
            return Ok(json);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting processed simulation data {ex.Message}");
        }

        return new ObjectResult(NotFound());
    }
    
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> GetAllProcessedData()
    {
        try
        {
            var documents = await _collection.FindAsync(_ => true);
            var results = documents.ToEnumerable().Select(doc => new
            {
                SettingsHash = doc["SettingsHash"].ToString(),
                MapHash = doc["MapHash"].ToString(),
                ChartType = doc["ChartType"].ToString(),
                RunId = doc["RunId"].ToString()
            }).Distinct().ToList();
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting processed simulation data {ex.Message}");
        }

        return new ObjectResult(NotFound());
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
            return StatusCode(500, $"Error while saving simulation data {ex.Message}");
        }
    }
    
    // delete processed data by hashes
    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ObjectResult> DeleteProcessedData([FromQuery] ProcessedDataQuery processedDataQuery)
    {
        try
        {
            var documents = await _collection.FindAsync(doc =>
                doc["MapHash"] == processedDataQuery.MapHash
                && doc["SettingsHash"] == processedDataQuery.SettingsHash
                && doc["RunId"] == processedDataQuery.RunId);
            var document = documents.FirstOrDefault();
            if (document != null)
            {
                await _collection.DeleteOneAsync(doc => doc["_id"] == document["_id"]);
            }
            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting processed simulation data {ex.Message}");
            return StatusCode(500, $"Error while deleting simulation data {ex.Message}");
        }
    }
}