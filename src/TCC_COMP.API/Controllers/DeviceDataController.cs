namespace TCC_COMP.API.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;
    using TCC_COMP.SERVICE.Interfaces;
    using System;
    using AutoMapper;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.DOMAIN.Entities;

    [Route("api/devicedata")]
    public class DeviceDataController : MainController
    {
        private readonly IDeviceDataService _dataService;
        private readonly IDeviceDataRepository _dataRepository;
        private readonly IMapper _mapper;

        public DeviceDataController(IDeviceDataService dataService,
                                    IMapper mapper,
                                    IDeviceDataRepository dataRepository,
                                    INotificador notificador): base(notificador)
        {
            _dataRepository = dataRepository;
            _dataService = dataService;
            _mapper = mapper;
        }
        
        [HttpGet("{device_id}")]
        public async Task<ActionResult<List<DeviceDataViewModel>>> ObterUltimos24Registros(string device_id)
        {
            try
            {
                var retorno = await _dataRepository.ObterUltimos24Registros(device_id);
                return CustomResponse(_mapper.Map<List<DeviceDataViewModel>>(retorno));
            }
            catch (Exception ex)
            {
                NotificarErro("Ocorreu uma exceção: " + ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(DeviceDataViewModel newdeviceData)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var retorno = await _dataService.Adicionar(_mapper.Map<DeviceData>(newdeviceData));
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro("Ocorreu uma exceção: " + ex.Message);
                return CustomResponse(ex.Message);
            }
        }
    }
}