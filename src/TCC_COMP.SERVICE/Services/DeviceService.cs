namespace TCC_COMP.SERVICE.Services
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;
    using TCC_COMP.SERVICE.Interfaces.Service;
    using TCC_COMP.SERVICE.ViewModels;
    using TCC_COMP.DOMAIN.Telegram;

    public class DeviceService : IDeviceService
    {

        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceDataRepository _dataRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly ITelegramRepository _telegramRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<Keys> _appSettings;

        public DeviceService(IDeviceRepository deviceRepository,
                             IMapper mapper,
                             IDeviceDataRepository dataRepository,
                             IPlantRepository plantRepository,
                             ITelegramRepository telegramRepository,
                             IOptions<Keys> appSettings)
        {
            _deviceRepository = deviceRepository;
            _dataRepository = dataRepository;
            _plantRepository = plantRepository;
            _telegramRepository = telegramRepository;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public async Task<List<Device>> ObterTodos()
        {
            var retorno = _mapper.Map<List<Device>>(await _deviceRepository.ObterTodos());

            foreach(var d in retorno)
            {
                d.plant_id = await _deviceRepository.ObterRelacaoPlanta(d.id);
            }

            foreach(var ret in retorno)
            {
                ret.deviceData = _mapper.Map<DeviceData>(await _dataRepository.ObterUltimoRegistro(ret.id));

                if (ret.deviceData != null)
                {
                    TimeSpan interval = DateTime.Now - Convert.ToDateTime(ret.deviceData.created_at);

                    if (interval.TotalDays < 1)
                    {
                        if (interval.Hours <= Convert.ToInt32(_appSettings.Value.IntervaloConnected))
                        {
                            ret.connected = "Conectado";
                        }
                        else
                        {
                            ret.connected = "Desconectado";
                        }
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

        public async Task<Device> ObterPorId(string device_id)
        {
            TimeSpan interval = new TimeSpan(Convert.ToInt32(_appSettings.Value.IntervaloConnected)+1, 0, 0);

            Device retorno = _mapper.Map<Device>(await _deviceRepository.ObterPorId(device_id));

            retorno.deviceData = _mapper.Map<DeviceData>(await _dataRepository.ObterUltimoRegistro(device_id));

            if(retorno.deviceData != null)
                 interval = DateTime.Now - Convert.ToDateTime(retorno.deviceData.created_at);

            if(interval.TotalDays < 1)
            {
                if (interval.Hours <= Convert.ToInt32(_appSettings.Value.IntervaloConnected))
                {
                    retorno.connected = "Conectado";
                }
                else
                {
                    retorno.connected = "Desconectado";
                }
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
                retorno.connected = "Desconectado";
                return retorno;
            }
        }

        public async Task<bool> AdicionarDevice(Device newDevice)
        {
            newDevice.updated_at = newDevice.created_at;
            newDevice.name = "Novo Jardim";

            bool retorno = true;

            var consultaExistente = await _deviceRepository.ObterPorId(newDevice.id);

            if(consultaExistente == null)
                retorno = await _deviceRepository.Adicionar(_mapper.Map<Device>(newDevice));

            if (newDevice.deviceData != null && retorno != false)
            {
                newDevice.deviceData.device_id = newDevice.id;
                newDevice.deviceData.created_at = DateTime.Now;
                retorno = await _dataRepository.Adicionar(_mapper.Map<DeviceData>(newDevice.deviceData));

                if(retorno == true)
                {
                    string plant_id = await _deviceRepository.ObterRelacaoPlanta(newDevice.id);

                    if (!string.IsNullOrEmpty(plant_id))
                    {
                        Plant dadosPlanta = await _plantRepository.ObterPlanta(Convert.ToInt32(plant_id));

                        NewMessage mensagem = new NewMessage();

                        if (newDevice.deviceData.air_humidity > dadosPlanta.air_humidity + 2) mensagem.text += "Humidade do ar acima do indicado. \n";
                        if (newDevice.deviceData.air_humidity < dadosPlanta.air_humidity - 2) mensagem.text += "Humidade do ar abaixo do indicado. \n";

                        if (newDevice.deviceData.air_temperature > dadosPlanta.air_temperature + 2) mensagem.text += "Temperatura do ar acima do indicado. \n";
                        if (newDevice.deviceData.air_temperature < dadosPlanta.air_temperature - 2) mensagem.text += "Temperatura do ar abaixo do indicado. \n";

                        if (newDevice.deviceData.soil_humidity > dadosPlanta.soil_humidity + 2) mensagem.text += "Humidade do solo acima do indicado. \n";
                        if (newDevice.deviceData.soil_humidity < dadosPlanta.soil_humidity - 2) mensagem.text += "Humidade do solo abaixo do indicado. \n";

                        if (newDevice.deviceData.solar_light > dadosPlanta.solar_light + 2) mensagem.text += "Luminosidade acima do indicado. \n";
                        if (newDevice.deviceData.soil_humidity < dadosPlanta.soil_humidity - 2) mensagem.text += "Luminosidade abaixo do indicado. \n";

                        if (!string.IsNullOrEmpty(mensagem.text))
                        {
                            mensagem.chat_id = await _telegramRepository.getChatId(newDevice.id);
                            if (mensagem.chat_id > 0 && !string.IsNullOrEmpty(mensagem.chat_id.ToString()))
                            {
                                await _telegramRepository.sendMessageAsync(mensagem);
                            }
                        }
                    }
                }

            }

            if (!string.IsNullOrEmpty(newDevice.plant_id))
                retorno = await _deviceRepository.IncluirRelacaoPlanta(newDevice.id, newDevice.plant_id);

            return retorno;
        }

        public async Task<string> AdicionarRelacaoPlantaDevice(Device includeRelation)
        {
            string retorno = string.Empty;

            if(!string.IsNullOrEmpty(includeRelation.id) && !string.IsNullOrEmpty(includeRelation.plant_id))
            {
                string plant_id = await _deviceRepository.ObterRelacaoPlanta(includeRelation.id);

                if (string.IsNullOrEmpty(plant_id))
                {
                    var ret = await _deviceRepository.IncluirRelacaoPlanta(includeRelation.id, includeRelation.plant_id);
                    retorno = ret.ToString();
                }
                else if(includeRelation.plant_id != plant_id)
                {
                    var ret = await _deviceRepository.AtualizarRelacaoPlanta(includeRelation.id, includeRelation.plant_id);
                    retorno = ret.ToString();
                }
                else
                {
                    retorno = "Esse device já está atrelado a este tipo de planta";
                }
                
            }

            return retorno;
        }

        public async Task<bool> AtualizarDevice(Device alteracaoDevice)
        {
            alteracaoDevice.updated_at = DateTime.Now;

            var retorno = await _deviceRepository.Atualizar(_mapper.Map<Device>(alteracaoDevice));

            if (!string.IsNullOrEmpty(alteracaoDevice.plant_id)) retorno = await _deviceRepository.AtualizarRelacaoPlanta(alteracaoDevice.id, alteracaoDevice.plant_id);

            return retorno;
        }

        public async Task<bool> DeletarDevice(string device_id)
        {
            bool retorno = false;

            var retornoDeleteData = await _dataRepository.Deletar(device_id);
            var retornoDeleteRelacaoPlanta = await _deviceRepository.DeletarRelacaoPlanta(device_id);
            var retornoDeleteFlag = await _telegramRepository.DeleteRelation(device_id);
            var retornoDeleteDevice = await _deviceRepository.Deletar(device_id);

            if (retornoDeleteDevice)
                retorno = true;
            
            return retorno;
        }
    }
}
