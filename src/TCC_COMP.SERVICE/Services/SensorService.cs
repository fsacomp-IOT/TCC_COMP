namespace TCC_COMP.SERVICE.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.SERVICE.ViewModels;
    using TCC_COMP.DOMAIN.Entities;
    using AutoMapper;

    public class SensorService : ISensorService
    {
        private readonly IMapper _mapper;
        private readonly ISensorRepository _sensorRepository;

        public SensorService(IMapper mapper, ISensorRepository sensorRepository)
        {
            _mapper = mapper;
            _sensorRepository = sensorRepository;
        }

        public async Task<List<SensorViewModel>> ObterTodosSensores()
        {
            return _mapper.Map<List<SensorViewModel>>(await _sensorRepository.ObterTodos());
        }

        public async Task<SensorViewModel> ObterSensorPorId(Guid id)
        {
            return _mapper.Map<SensorViewModel>(await _sensorRepository.ObterPorId(id));
        }

        public async Task<bool> AdicionarSensor(SensorViewModel newSensor)
        {
            newSensor.Created_At = DateTime.Now;
            newSensor.Sensor_Id = Guid.NewGuid();

            return await _sensorRepository.Adicionar(_mapper.Map<Sensor>(newSensor));
        }

        public Task<bool> AtualizarSensor(Guid id, SensorViewModel alteracaoSensor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarSensor(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}