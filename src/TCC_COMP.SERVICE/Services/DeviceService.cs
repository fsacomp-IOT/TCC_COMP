namespace TCC_COMP.SERVICE.Services
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;

    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        public async Task<List<DeviceViewModel>> ObterTodosDevices()
        {
            var retorno = _mapper.Map<List<DeviceViewModel>>(await _deviceRepository.ObterTodos());

            if (retorno.Count != 0)
            {
                return retorno;
            }
            else
            {
                return new List<DeviceViewModel>();
            }
        }

        public async Task<Device> ObterDevicePorId(Guid device_id)
        {
            var retorno = await _deviceRepository.ObterPorId(device_id);

            if (retorno != null)
            {
                return retorno;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método para criação de novos Devices.
        /// </summary>
        /// <param name="newDevice">newDevice.</param>
        /// <returns>Device.</returns>
        public async Task<Device> AdicionarDevice(Device newDevice)
        {
            var retorno = await this._deviceRepository.Adicionar(newDevice);

            if (retorno != null)
            {
                return await this._deviceRepository.ObterPorId(Guid.Parse(retorno));
            }

            return null;
        }

        public async Task<Device> AtualizarDevice(Guid device_id, Device alteracaoDevice)
        {
            alteracaoDevice.Updated_at = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            alteracaoDevice.Device_id = device_id;

            var retorno = await _deviceRepository.Atualizar(alteracaoDevice);

            if (retorno)
            {
                Device NovosDados = await _deviceRepository.ObterPorId(device_id);
                return NovosDados;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeletarDevice(Guid device_id)
        {
            var retorno = await _deviceRepository.Deletar(device_id);

            return retorno;
        }
    }
}
