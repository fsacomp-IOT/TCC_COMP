namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    using System;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;

    public interface IDeviceRepository : IRepository<Device>
    {
        Task<Device> ObterPorId(Guid id);
        Task<bool> Deletar(Guid device_id);
    }
}
