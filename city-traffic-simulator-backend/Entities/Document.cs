namespace city_traffic_simulator_backend.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

public class Document
{
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.String)]
    [BsonId(IdGenerator = typeof(GuidGenerator))]
    public Guid? Id { get; set; }
}