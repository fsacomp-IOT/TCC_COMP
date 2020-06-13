namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.ViewModels;

    public interface ISensorTypeService
    {
        Task<List<SensorTypeViewModel>> ObterTodosSensorTypes();
        Task<SensorTypeViewModel> ObterSensorTypePorId(int sensor_type_id);
        Task<bool> AdicionarSensorType(SensorTypeViewModel newSensorType);
        Task<bool> AtualizarSensorType(int sensor_type_id, SensorTypeViewModel alteracaoSensorType);
        Task<bool> DeletarSensorType(int sensor_type_id);
    }
}
