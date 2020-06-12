namespace TCC_COMP.SERVICE.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.SERVICE.Interfaces.Service;

    public class SensorTypeService : ISensorTypeService
    {
        private readonly ISensorTypeRepository _sensorTypeRepository;

        public SensorTypeService(ISensorTypeRepository sensorTypeRepository)
        {
            _sensorTypeRepository = sensorTypeRepository;
        }

        public async Task<List<SensorType>> ObterTodosSensorTypes()
        {
            var SensorTypes = await _sensorTypeRepository.ObterTodos();

            if (SensorTypes.Count > 0)
            {
                return SensorTypes;
            }
            else
            {
                return new List<SensorType>();
            }
        }

        public async Task<SensorType> ObterSensorTypePorId(int sensor_type_id)
        {
            var SensorType = await _sensorTypeRepository.ObterPorId(sensor_type_id);

            if (SensorType != null)
            {
                return SensorType;
            }
            else
            {
                return new SensorType();
            }
        }

        public async Task<SensorType> AdicionarSensorType(SensorType newSensorType)
        {
            var retorno = await _sensorTypeRepository.Adicionar(newSensorType);

            if (retorno != "0")
            {
                return await _sensorTypeRepository.ObterPorId(Convert.ToInt32(retorno));
            }

            return new SensorType();
        }

        public async Task<SensorType> AtualizarSensorType(int sensor_type_id, SensorType alteracaoSensorType)
        {
            alteracaoSensorType.Sensor_type_id = sensor_type_id;
            alteracaoSensorType.Updated_at = DateTime.Now;

            var retorno = await _sensorTypeRepository.Atualizar(alteracaoSensorType);

            if (retorno == true)
            {
                SensorType Alteracao = await _sensorTypeRepository.ObterPorId(sensor_type_id);
                return Alteracao;
            }
            else
            {
                return new SensorType();
            }
        }

        public async Task<SensorType> DeletarSensorType(int sensor_type_id)
        {
            var retorno = await _sensorTypeRepository.Deletar(sensor_type_id);

            return await _sensorTypeRepository.ObterPorId(sensor_type_id);
        }
    }
}
