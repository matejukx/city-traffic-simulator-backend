namespace city_traffic_simulator_backend.Entities.Dto;

using System.Text.Json.Serialization;

public class Car
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("state")]
    public string? State { get; set; }
    
    [JsonPropertyName("velocity")]
    public Vector2d? Velocity { get; set; }
    
    [JsonPropertyName("acceleration")]
    public Vector2d? Acceleration { get; set; }
    
    [JsonPropertyName("position")]
    public Vector2d? Position { get; set; }
    
    [JsonPropertyName("distanceToPrecedingCar")]
    public float? DistanceToPrecedingCar { get; set; }
    
    [JsonPropertyName("precedingCarId")]
    public int? PrecedingCarId { get; set; }
}