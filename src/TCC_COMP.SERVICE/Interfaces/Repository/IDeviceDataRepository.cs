namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using TCC_COMP.DOMAIN.Entities;

    public interface IDeviceDataRepository
    {
        Task<List<DeviceData>> ObterUltimos24Registros(string id);
        Task<DeviceData> ObterUltimoRegistro(string id);
        Task<bool> Adicionar(DeviceData newDeviceData);
        Task<bool> Deletar(string device_id);
    }
}