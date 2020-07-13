namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using TCC_COMP.DOMAIN.Entities;

    public interface IDeviceRepository
    {
        Task<List<Device>> ObterTodos();
        Task<Device> ObterPorId(string id);
        Task<bool> Adicionar(Device newDevice);
        Task<bool> Atualizar(string id, Device updatedDevice);
        Task<bool> Deletar(string device_id);
    }
}
