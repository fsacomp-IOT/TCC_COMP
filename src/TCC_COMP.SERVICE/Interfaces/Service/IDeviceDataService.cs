namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using TCC_COMP.DOMAIN.Entities;

    public interface IDeviceDataService
    {
        Task<List<DeviceData>> ObterUltimos24Registros(string id);
        Task<bool> Adicionar(DeviceData newDeviceData);
    }
}