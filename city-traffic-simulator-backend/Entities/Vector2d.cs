namespace city_traffic_simulator_backend.Entities;

using System.Text.Json.Serialization;

public class Vector2d
{
    [JsonPropertyName("x")]
    public double X { get; set; }
    
    [JsonPropertyName("y")]
    public double Y { get; set; }
}