namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;

    public interface ISensorTypeRepository : IRepository<SensorType>
    {
        Task<SensorType> ObterPorId(int sensor_type_id);
        Task<bool> Deletar(int sensor_type_id);
    }
}
