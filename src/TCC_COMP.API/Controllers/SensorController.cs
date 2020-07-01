namespace TCC_COMP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;

    [Route("api/sensor")]
    public class SensorController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ISensorService _sensorService;

        public SensorController(IMapper mapper, ISensorService sensorSensor)
        {
            _mapper = mapper;
            _sensorService = sensorSensor;
        }

        [HttpGet]
        public async Task<ActionResult<List<SensorViewModel>>> ObterTodos()
        {
            var Sensors = await _sensorService.ObterTodosSensores();

            if(Sensors.Count > 0)
            {
                return Ok(Sensors);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{ID:Guid}")]
        public async Task<ActionResult<SensorViewModel>> ObterPorId(Guid ID)
        {
            var Sensor = await _sensorService.ObterSensorPorId(ID);

            if(Sensor != null)
            {
                return Ok(Sensor);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Salvar(SensorViewModel newSensor)
        {
            var retorno = await _sensorService.AdicionarSensor(newSensor);

            if(retorno != false)
            {
                return Ok(retorno);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}