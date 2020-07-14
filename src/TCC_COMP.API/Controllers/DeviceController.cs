namespace TCC_COMP.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;
    using System.Collections.Generic;

    [Route("api/devices")]
    public class DeviceController : MainController
    {
        private readonly IDeviceService _deviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="deviceService">Serviços de Device.</param>
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Retorna uma lista com todos os dispositivos ativos.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        public async Task<ActionResult<List<DeviceViewModel>>> ObterTodos()
        {
            var retorno = await _deviceService.ObterTodosDevices();

            if (retorno != null)
            {
                return Ok(retorno);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{device_id}")]
        public async Task<ActionResult<DeviceViewModel>> ObterPorId(string device_id)
        {
            var retorno = await _deviceService.ObterDevicePorId(device_id);

            if (retorno.name != null)
            {
                return Ok(retorno);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Salvar(DeviceViewModel newDevice)
        {
            var retorno = await _deviceService.AdicionarDevice(newDevice);

            if (retorno)
            {
                return Ok(retorno);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{device_id}")]
        public async Task<ActionResult<DeviceViewModel>> Atualizar(string device_id, DeviceViewModel alteracaoDevice)
        {
            var retorno = await _deviceService.AtualizarDevice(device_id, alteracaoDevice);

            if (retorno)
            {
                return Ok(retorno);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{device_id}")]
        public async Task<ActionResult<string>> DeletarDevice(string device_id)
        {
            var retorno = await _deviceService.DeletarDevice(device_id);

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
