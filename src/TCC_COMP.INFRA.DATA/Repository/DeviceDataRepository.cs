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
                                    :base(configuration)
        {
        }

        public async Task<List<DeviceData>> ObterUltimos24Registros(string device_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device_Data\" WHERE device_id = @device_id ORDER BY id DESC FETCH FIRST 24 ROWS ONLY";

            using(var connection = new NpgsqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var retorno = await connection.QueryAsync<DeviceData>(command, new { device_id = device_id});

                    return retorno.ToList();
                }
                catch (Exception)
                {
                    return new List<DeviceData>();
                }
                finally
                {
                    if(connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<DeviceData> ObterUltimoRegistro(string device_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device_Data\" WHERE device_id = @device_id ORDER BY id DESC FETCH FIRST 1 ROWS ONLY";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var retorno = await connection.QueryAsync<DeviceData>(command, new { device_id = device_id });

                    return retorno.FirstOrDefault();
                }
                catch (Exception e)
                {
                    return new DeviceData();
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<bool> Adicionar(DeviceData newDeviceData)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.AddDynamicParams(new {
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
                await connection.OpenAsync();

                using(var trans = connection.BeginTransaction())
                {
                    try
                    {
                        var retorno = await connection.ExecuteAsync(command, dynamicParameters);

                        trans.Commit();

                        if(retorno != 0)
                        {
                            return true;
                        }

                        return false;
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                    finally
                    {
                        if(connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}