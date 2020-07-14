namespace TCC_COMP.SERVICE.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.SERVICE.ViewModels;
    using TCC_COMP.DOMAIN.Entities;
    using AutoMapper;

    public class DeviceDataService : IDeviceDataService
    {
        private readonly IMapper _mapper;
        private readonly IDeviceDataRepository _dataRepository;

        public DeviceDataService(IMapper mapper, IDeviceDataRepository dataRepository)
        {
            _mapper = mapper;
            _dataRepository = dataRepository;
        }

        public async Task<List<DeviceDataViewModel>> ObterUltimos24Registros(string id)
        {
            return _mapper.Map<List<DeviceDataViewModel>>(await _dataRepository.ObterUltimos24Registros(id));
        }

        public async Task<bool> Adicionar(DeviceDataViewModel newDeviceData)
        {
            return await _dataRepository.Adicionar(_mapper.Map<DeviceData>(newDeviceData));
        }
    }
}