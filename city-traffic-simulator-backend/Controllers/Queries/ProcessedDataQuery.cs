namespace city_traffic_simulator_backend.Controllers.Queries;

public class ProcessedDataQuery
{
    public string SettingsHash { get; set; }
    
    public string MapHash { get; set; }
    
    public int? RunId { get; set; }
    
    public string ChartType { get; set; }
}