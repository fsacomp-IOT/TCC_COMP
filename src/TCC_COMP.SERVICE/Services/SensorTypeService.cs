namespace TCC_COMP.SERVICE.Services
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;

    public class SensorTypeService : ISensorTypeService
    {
        private readonly ISensorTypeRepository _sensorTypeRepository;
        private readonly IMapper _mapper;

        public SensorTypeService(ISensorTypeRepository sensorTypeRepository, IMapper mapper)
        {
            _sensorTypeRepository = sensorTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<SensorTypeViewModel>> ObterTodosSensorTypes()
        {
            var SensorTypes = _mapper.Map<List<SensorTypeViewModel>>(await _sensorTypeRepository.ObterTodos());

            if (SensorTypes.Count > 0)
            {
                return SensorTypes;
            }
            else
            {
                return new List<SensorTypeViewModel>();
            }
        }

        public async Task<SensorTypeViewModel> ObterSensorTypePorId(int sensor_type_id)
        {
            var SensorType = _mapper.Map<SensorTypeViewModel>(await _sensorTypeRepository.ObterPorId(sensor_type_id));

            if (SensorType != null)
            {
                return SensorType;
            }
            else
            {
                return new SensorTypeViewModel();
            }
        }

        public async Task<bool> AdicionarSensorType(SensorTypeViewModel newSensorType)
        {
           return await _sensorTypeRepository.Adicionar(_mapper.Map<SensorType>(newSensorType));
        }

        public async Task<bool> AtualizarSensorType(int sensor_type_id, SensorTypeViewModel alteracaoSensorType)
        {
            alteracaoSensorType.Sensor_Type_Id = sensor_type_id;
            alteracaoSensorType.Updated_At = DateTime.Now.ToString();

            return await _sensorTypeRepository.Atualizar(_mapper.Map<SensorType>(alteracaoSensorType));
        }

        public async Task<bool> DeletarSensorType(int sensor_type_id)
        {
            return await _sensorTypeRepository.Deletar(sensor_type_id);
        }
    }
}
