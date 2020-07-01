namespace TCC_COMP.SERVICE.Interfaces.Service
{
    using System;
    using TCC_COMP.SERVICE.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISensorService
    {
         Task<List<SensorViewModel>> ObterTodosSensores();
         Task<SensorViewModel> ObterSensorPorId(Guid id);
         Task<bool> AdicionarSensor(SensorViewModel newSensor);
         Task<bool> AtualizarSensor(Guid id, SensorViewModel alteracaoSensor);
         Task<bool> DeletarSensor(Guid id);
    }
}