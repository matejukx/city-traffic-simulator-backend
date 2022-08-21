namespace city_traffic_simulator_backend.Entities.Dto;

using System.Text.Json.Serialization;

public class Frame
{
    [JsonPropertyName("frameNumber")]
    public int FrameNumber { get; set; }
    
    [JsonPropertyName("cars")]
    public Car[] Cars { get; set; }
}