namespace TCC_COMP.API.Configurations
{
    using AutoMapper;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.ViewModels;

    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<DeviceViewModel, Device>().ReverseMap();
            CreateMap<SensorViewModel, Sensor>().ReverseMap();
            CreateMap<SensorEventViewModel, SensorEvent>().ReverseMap();
            CreateMap<SensorTypeViewModel, SensorType>().ReverseMap();
        }
    }
}
