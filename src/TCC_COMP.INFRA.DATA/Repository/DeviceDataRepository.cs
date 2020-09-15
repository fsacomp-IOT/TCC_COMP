namespace TCC_COMP.INFRA.DATA.Repository
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;

    public class DeviceDataRepository : BaseRepository<DeviceData>, IDeviceDataRepository
    {
        private string command = string.Empty;

        public DeviceDataRepository(IConfiguration configuration)
                                    : base(configuration)
        {
        }

        #region [SELECT]

        public async Task<List<DeviceData>> ObterUltimos24Registros(string device_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device_Data\" WHERE device_id = @device_id ORDER BY id DESC FETCH FIRST 24 ROWS ONLY";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<DeviceData>(command, new { device_id = device_id });

                    return retorno.ToList();
                }
                catch (TimeoutException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu um timeout", GetType().FullName), ex);
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu uma exceção SQL Mensagem: {1}", GetType().FullName, ex.Message), ex);
                }
            }
        }

        public async Task<DeviceData> ObterUltimoRegistro(string device_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device_Data\" WHERE device_id = @device_id ORDER BY id DESC FETCH FIRST 1 ROWS ONLY";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<DeviceData>(command, new { device_id = device_id });

                    return retorno.FirstOrDefault();
                }
                catch (TimeoutException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu um timeout", GetType().FullName), ex);
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu uma exceção SQL Mensagem: {1}", GetType().FullName, ex.Message), ex);
                }
            }
        }

        #endregion

        #region [INSERT]

        public async Task<bool> Adicionar(DeviceData newDeviceData)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.AddDynamicParams(new
            {
                newDeviceData.created_at,
                newDeviceData.device_id,
                newDeviceData.soil_humidity,
                newDeviceData.air_humidity,
                newDeviceData.air_temperature,
                newDeviceData.solar_light
            });

            command = "INSERT INTO \"TCC_COMP\".\"Device_Data\" (device_id, created_at, soil_humidity, air_humidity, air_temperature, solar_light) VALUES (@device_id, @created_at, @soil_humidity, @air_humidity, @air_temperature, @solar_light)";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = connection.BeginTransaction())
                    {

                        var retorno = await connection.ExecuteAsync(command, dynamicParameters);

                        await trans.CommitAsync();

                        if (retorno != 0)
                        {
                            return true;
                        }

                        return false;
                    }
                }
                catch (TimeoutException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu um timeout", GetType().FullName), ex);
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu uma exceção SQL Mensagem: {1}", GetType().FullName, ex.Message), ex);
                }
            }
        }

        #endregion

    }
}