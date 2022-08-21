namespace city_traffic_simulator_backend.Entities;

using Dto;

public class SimulationDataDocument : Document
{
    public string SettingsHash { get; set; }
    
    public string MapHash { get; set; }
    
    public Frame[] Frames { get; set; }
}