namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.SERVICE.ViewModels;

    public interface IDeviceService
    {
        Task<List<DeviceViewModel>> ObterTodosDevices();
        Task<DeviceViewModel> ObterDevicePorId(string device_id);
        Task<bool> AdicionarDevice(DeviceViewModel newDevice);
        Task<bool> AtualizarDevice(string device_id, DeviceViewModel updatedDevice);
        Task<bool> DeletarDevice(string device_id);
    }
}
