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
        private readonly IDeviceDataRepository _dataRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper, IDeviceDataRepository dataRepository)
        {
            _deviceRepository = deviceRepository;
            _dataRepository = dataRepository;
            _mapper = mapper;
        }

        public async Task<List<Device>> ObterTodosDevices()
        {
            var retorno = _mapper.Map<List<Device>>(await _deviceRepository.ObterTodos());

            foreach(var ret in retorno)
            {
                ret.deviceData = _mapper.Map<DeviceData>(await _dataRepository.ObterUltimoRegistro(ret.id));

                if (ret.deviceData != null)
                {
                    TimeSpan interval = DateTime.Now - Convert.ToDateTime(ret.deviceData.created_at);

                    if (interval.Hours <= 3)
                    {
                        ret.connected = "Conectado";
                    }
                    else
                    {
                        ret.connected = "Desconectado";
                    }
                }
                
            }

            if (retorno.Count != 0)
            {
                return retorno;
            }
            else
            {
                return new List<Device>();
            }
        }

        public async Task<Device> ObterDevicePorId(string device_id)
        {
            Device retorno = _mapper.Map<Device>(await _deviceRepository.ObterPorId(device_id));

            retorno.deviceData = _mapper.Map<DeviceData>(await _dataRepository.ObterUltimoRegistro(device_id));

            TimeSpan interval = DateTime.Now - Convert.ToDateTime(retorno.deviceData.created_at);

            if(interval.Hours <= 3)
            {
                retorno.connected = "Conectado";
            }
            else
            {
                retorno.connected = "Desconectado";
            }

            if (retorno != null)
            {
                return retorno;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AdicionarDevice(Device newDevice)
        {
            newDevice.updated_at = newDevice.created_at;

            bool retorno = true;

            var consultaExistente = await _deviceRepository.ObterPorId(newDevice.id);

            if(consultaExistente == null)
                retorno = await _deviceRepository.Adicionar(_mapper.Map<Device>(newDevice));

            if (newDevice.deviceData != null && retorno != false)
            {
                newDevice.deviceData.device_id = newDevice.id;
                newDevice.deviceData.created_at = DateTime.Now;
                retorno = await _dataRepository.Adicionar(_mapper.Map<DeviceData>(newDevice.deviceData));
            }

            return retorno;
        }

        public async Task<bool> AtualizarDevice(Device alteracaoDevice)
        {
            alteracaoDevice.updated_at = DateTime.Now;

            return await _deviceRepository.Atualizar(_mapper.Map<Device>(alteracaoDevice));
        }

        public async Task<bool> DeletarDevice(string device_id)
        {
            var retorno = await _deviceRepository.Deletar(device_id);

            return retorno;
        }
    }
}
