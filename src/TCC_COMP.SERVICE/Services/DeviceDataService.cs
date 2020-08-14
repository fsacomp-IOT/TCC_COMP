namespace TCC_COMP.SERVICE.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.DOMAIN.Entities;
    using AutoMapper;

    public class DeviceDataService : IDeviceDataService
    {
        private readonly IDeviceDataRepository _dataRepository;

        public DeviceDataService(IMapper mapper, IDeviceDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<List<DeviceData>> ObterUltimos24Registros(string id)
        {
            return await _dataRepository.ObterUltimos24Registros(id);
        }

        public async Task<bool> Adicionar(DeviceData newDeviceData)
        {
            return await _dataRepository.Adicionar(newDeviceData);
        }
    }
}