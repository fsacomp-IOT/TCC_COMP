namespace TCC_COMP.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;
    using System.Collections.Generic;
    using TCC_COMP.SERVICE.Interfaces;
    using System;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.DOMAIN.Entities;
    using AutoMapper;

    [Route("api/devices")]
    public class DeviceController : MainController
    {
        private readonly IDeviceService _deviceService;
        private readonly IDeviceRepository _deviceRepository;
        private readonly ITelegramRepository _telegramRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="deviceService">Serviços de Device.</param>
        public DeviceController(IDeviceService deviceService,
                                IDeviceRepository deviceRepository,
                                IMapper mapper,
                                INotificador notificador) : base(notificador)
        {
            _deviceService = deviceService;
            _mapper = mapper;
            _deviceRepository = deviceRepository;
        }

        /// <summary>
        /// Retorna uma lista com todos os dispositivos ativos.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        public async Task<ActionResult<List<DeviceViewModel>>> ObterTodos()
        {
            try
            {
                var retorno = _mapper.Map<List<DeviceViewModel>>(await _deviceService.ObterTodos());
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpGet("{device_id}")]
        public async Task<ActionResult<DeviceViewModel>> ObterPorId(string device_id)
        {
            try
            {
                var retorno = _mapper.Map<DeviceViewModel>(await _deviceService.ObterPorId(device_id));
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DeviceViewModel>> Salvar(DeviceViewModel newDevice)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var retorno = await _deviceService.AdicionarDevice(_mapper.Map<Device>(newDevice));

                return CustomResponse(newDevice);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpPost("/incluirRelacao")]
        public async Task<ActionResult<DeviceViewModel>> IncluirRelacaoPlanta(DeviceViewModel includeRelation)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var retorno = await _deviceService.AdicionarRelacaoPlantaDevice(_mapper.Map<Device>(includeRelation));

                return CustomResponse(includeRelation);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpPut("{device_id}")]
        public async Task<ActionResult<DeviceViewModel>> Atualizar(string device_id, DeviceViewModel alteracaoDevice)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (device_id != alteracaoDevice.id) return BadRequest();

            try
            {
                var retorno = await _deviceService.AtualizarDevice(_mapper.Map<Device>(alteracaoDevice));
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpDelete("{device_id}")]
        public async Task<ActionResult<string>> DeletarDevice(string device_id)
        {
            try
            {
                var retorno = await _deviceService.DeletarDevice(device_id);
                return CustomResponse();
            }
            catch (Exception ex)
            {
                NotificarErro("Ocorreu uma exceção: " + ex.Message);
                return CustomResponse(ex.Message);
            }
        }
    }
}
