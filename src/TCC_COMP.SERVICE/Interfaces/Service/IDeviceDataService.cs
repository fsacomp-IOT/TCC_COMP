namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using TCC_COMP.SERVICE.ViewModels;

    public interface IDeviceDataService
    {
        Task<List<DeviceDataViewModel>> ObterUltimos24Registros(string id);
        Task<bool> Adicionar(DeviceDataViewModel newDeviceData);
    }
}