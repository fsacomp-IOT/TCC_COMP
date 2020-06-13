﻿namespace TCC_COMP.SERVICE.Services
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

        public async Task<DeviceViewModel> ObterDevicePorId(Guid device_id)
        {
            var retorno = _mapper.Map<DeviceViewModel>(await _deviceRepository.ObterPorId(device_id));

            if (retorno != null)
            {
                return retorno;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AdicionarDevice(DeviceViewModel newDevice)
        {
            return await _deviceRepository.Adicionar(_mapper.Map<Device>(newDevice));
        }

        public async Task<bool> AtualizarDevice(Guid device_id, DeviceViewModel alteracaoDevice)
        {
            alteracaoDevice.Updated_At = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            alteracaoDevice.Device_Id = device_id;

            return await _deviceRepository.Atualizar(_mapper.Map<Device>(alteracaoDevice));
        }

        public async Task<bool> DeletarDevice(Guid device_id)
        {
            var retorno = await _deviceRepository.Deletar(device_id);

            return retorno;
        }
    }
}
