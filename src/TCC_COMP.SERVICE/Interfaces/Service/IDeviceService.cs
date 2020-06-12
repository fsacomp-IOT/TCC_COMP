namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.ViewModels;

    public interface IDeviceService
    {
        Task<List<DeviceViewModel>> ObterTodosDevices();
        Task<Device> ObterDevicePorId(Guid device_id);
        Task<Device> AdicionarDevice(Device newDevice);
        Task<Device> AtualizarDevice(Guid device_id, Device alteracaoDevice);
        Task<bool> DeletarDevice(Guid device_id);
    }
}
