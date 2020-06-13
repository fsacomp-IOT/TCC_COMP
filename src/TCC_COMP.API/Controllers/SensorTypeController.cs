namespace TCC_COMP.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;

    [Route("api/sensortype")]
    public class SensorTypeController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ISensorTypeService _sensorTypeService;

        public SensorTypeController(IMapper mapper,
                                    ISensorTypeService sensorTypeService)
        {
            _mapper = mapper;
            _sensorTypeService = sensorTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SensorTypeViewModel>>> ObterTodos()
        {
            var SensorTypes = _mapper.Map<List<SensorTypeViewModel>>(await _sensorTypeService.ObterTodosSensorTypes());

            if (SensorTypes.Count > 0)
            {
                return Ok(SensorTypes);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpGet("{sensor_type_id:int}")]
        public async Task<ActionResult<SensorTypeViewModel>> ObterPorId(int sensor_type_id)
        {
            var SensorType = _mapper.Map<SensorTypeViewModel>(await _sensorTypeService.ObterSensorTypePorId(sensor_type_id));

            if (SensorType != null)
            {
                return Ok(SensorType);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPost]
        public async Task<ActionResult<bool>> Salvar(SensorTypeViewModel newSensorType)
        {
            var retorno = await _sensorTypeService.AdicionarSensorType(newSensorType);

            if (retorno)
            {
                return Ok(retorno);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{sensor_type_id:int}")]
        public async Task<ActionResult<bool>> Atualizar(int sensor_type_id, SensorTypeViewModel atualizacaoSensorType)
        {
            var retorno = await _sensorTypeService.AtualizarSensorType(sensor_type_id, atualizacaoSensorType);

            if (retorno)
            {
                return Ok(retorno);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{sensor_type_id:int}")]
        public async Task<ActionResult<bool>> Deletar(int sensor_type_id)
        {
            var retorno = await _sensorTypeService.DeletarSensorType(sensor_type_id);

            if (retorno)
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
