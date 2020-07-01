namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.SERVICE.ViewModels;

    public interface IDeviceService
    {
        Task<List<DeviceViewModel>> ObterTodosDevices();
        Task<DeviceViewModel> ObterDevicePorId(Guid device_id);
        Task<bool> AdicionarDevice(DeviceViewModel newDevice);
        Task<bool> AtualizarDevice(Guid device_id, DeviceViewModel alteracaoDevice);
        Task<bool> DeletarDevice(Guid device_id);
    }
}
