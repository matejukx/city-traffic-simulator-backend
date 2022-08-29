namespace city_traffic_simulator_backend.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Repository;

[Route("test")]
public class TestController : ControllerBase
{
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly ILogger<TestController> _logger;

    public TestController(MongoContext context, ILogger<TestController> logger)
    {
        _logger = logger;
        _collection = context.Database.GetCollection<BsonDocument>("testData");
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ObjectResult> GetTestData([FromRoute] string id)
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
    public async Task<ObjectResult> SaveTestData([FromBody] JsonElement content)
    {
        try
        {
            var json = content.GetRawText();
            var document = BsonSerializer.Deserialize<BsonDocument>(json);

            await _collection.InsertOneAsync(document);
            return CreatedAtAction(nameof(SaveTestData),
                new { id = document["_id"].ToString()});
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while saving processed simulation data {ex.Message}");
            return this.StatusCode(500, "Error while saving simulation data");
        }
    }
    
    // [HttpGet]
    // public async Task<ObjectResult> GetAllTestData()
    // {
    //     try
    //     {
    //         var documents = await _collection.Find(_ => true).ToListAsync();
    //         var data = documents.Select(doc => doc.ToJson());
    //         return this.Ok(data);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError($"Error while getting processed simulation data {ex.Message}");
    //     }
    //
    //     return new ObjectResult(this.NotFound());
    // }
}