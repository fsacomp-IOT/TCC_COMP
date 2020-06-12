using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCC_COMP.DOMAIN.Entities;
using TCC_COMP.SERVICE.Interfaces.Service;
using TCC_COMP.SERVICE.ViewModels;

namespace TCC_COMP.API.Controllers
{
    [Route("api/devices")]
    public class DeviceController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="mapper">AutoMapper.</param>
        /// <param name="deviceService">Serviços de Device.</param>
        public DeviceController(IMapper mapper, IDeviceService deviceService)
        {
            _mapper = mapper;
            _deviceService = deviceService;
        }

        /// <summary>
        /// Retorna uma lista com todos os dispositivos ativos.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        public async Task<ActionResult<DeviceViewModel>> ObterTodos()
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

        [HttpGet("{device_id:guid}")]
        public async Task<ActionResult<DeviceViewModel>> ObterPorId(Guid device_id)
        {
            var retorno = _mapper.Map<DeviceViewModel>(await _deviceService.ObterDevicePorId(device_id));

            if (retorno.Device_Name != null)
            {
                return Ok(retorno);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<DeviceViewModel>> Salvar(DeviceViewModel newDevice)
        {
            var retorno = await _deviceService.AdicionarDevice(_mapper.Map<Device>(newDevice));

            if (retorno != null)
            {
                newDevice = _mapper.Map<DeviceViewModel>(retorno);
                return CreatedAtAction("Salvar", newDevice);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{device_id:guid}")]
        public async Task<ActionResult<DeviceViewModel>> Atualizar(Guid device_id, DeviceViewModel alteracaoDevice)
        {
            var retorno = await _deviceService.AtualizarDevice(device_id, _mapper.Map<Device>(alteracaoDevice));

            if (retorno != null)
            {
                alteracaoDevice = _mapper.Map<DeviceViewModel>(retorno);

                return Ok(alteracaoDevice);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{device_id:guid}")]
        public async Task<ActionResult<string>> DeletarDevice(Guid device_id)
        {
            var retorno = await _deviceService.DeletarDevice(device_id);

            if (retorno)
            {
                return Ok("Dispositivo excluido com sucesso");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
