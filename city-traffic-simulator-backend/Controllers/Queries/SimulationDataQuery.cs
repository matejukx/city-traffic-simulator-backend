//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SimulationDataQuery.cs" company="automotiveMastermind">
//    © automotiveMastermind. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace city_traffic_simulator_backend.Controllers.Queries;

public class SimulationDataQuery
{
    public string SettingsHash { get; set; }
    
    public string MapHash { get; set; }
}