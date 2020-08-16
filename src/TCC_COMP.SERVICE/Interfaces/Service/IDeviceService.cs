namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;

    public interface IDeviceService
    {
        Task<List<Device>> ObterTodos();
        Task<Device> ObterPorId(string device_id);
        Task<bool> AdicionarDevice(Device newDevice);
        Task<bool> AtualizarDevice(Device updatedDevice);
        Task<bool> DeletarDevice(string device_id);
    }
}
