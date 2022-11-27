namespace city_traffic_simulator_backend.Entities;

public class Settings : Document
{
    public string Hash { get; set; }
    public List<Tag> Tags { get; set; }
}