namespace city_traffic_simulator_backend.Entities;

public class Map : Document
{
    public string Hash { get; set; }
    public List<Road> Roads { get; set; }
}