namespace city_traffic_simulator_backend;

using AutoMapper;
using Entities;
using Entities.Dto;

public class SimulationMapperProfile : Profile
{
    public SimulationMapperProfile()
    {
        this.CreateMap<SimulationChunk, SimulationDataDocument>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ReverseMap();
    }
}