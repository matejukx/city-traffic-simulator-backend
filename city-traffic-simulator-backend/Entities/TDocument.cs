namespace city_traffic_simulator_backend.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Document
{
    [BsonId]
    [BsonElement("id")]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
}