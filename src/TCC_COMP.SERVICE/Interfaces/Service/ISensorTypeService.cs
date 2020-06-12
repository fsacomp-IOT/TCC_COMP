namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;

    public interface ISensorTypeService
    {
        Task<List<SensorType>> ObterTodosSensorTypes();
        Task<SensorType> ObterSensorTypePorId(int sensor_type_id);
        Task<SensorType> AdicionarSensorType(SensorType newSensorType);
        Task<SensorType> AtualizarSensorType(int sensor_type_id, SensorType alteracaoSensorType);
        Task<SensorType> DeletarSensorType(int sensor_type_id);
    }
}
