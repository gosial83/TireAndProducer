using AutoMapper;
using TireAndProducer.Library.Models;
using TireAndProducerAPI.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Producer, ProducerViewModel>();
        CreateMap<ProducerViewModel, Producer>();

        CreateMap<Tire, TireViewModel>();
        CreateMap<TireViewModel, Tire>();
    }
}