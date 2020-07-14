namespace TCC_COMP.API.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;

    [Route("api/devicedata")]
    public class DeviceDataController : MainController
    {
        private readonly IDeviceDataService _dataService;

        public DeviceDataController(IDeviceDataService dataService)
        {
            _dataService = dataService;            
        }
        
        [HttpGet("{device_id}")]
        public async Task<ActionResult<List<DeviceDataViewModel>>> ObterUltimos24Registros(string device_id)
        {
            var retorno = await _dataService.ObterUltimos24Registros(device_id); 

            if (retorno != null)
            {
                return Ok(retorno);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(DeviceDataViewModel newdeviceData)
        {
            var retorno = await _dataService.Adicionar(newdeviceData);

            if (retorno)
            {
                return Ok(retorno);
            }
            else
            {
                return NoContent();
            }
        }
    }
}