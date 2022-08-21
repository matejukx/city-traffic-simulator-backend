namespace city_traffic_simulator_backend.Entities.Dto;

using System.Text.Json.Serialization;

public class SimulationChunk
{
    [JsonPropertyName("settingsHash")]
    public string SettingsHash { get; set; }
    
    [JsonPropertyName("mapHash")]
    public string MapHash { get; set; }
    
    [JsonPropertyName("frames")]
    public Frame[] Frames { get; set; }
} 