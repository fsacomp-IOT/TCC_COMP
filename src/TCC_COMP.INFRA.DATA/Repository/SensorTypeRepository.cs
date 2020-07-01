namespace TCC_COMP.INFRA.DATA.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;

    public class SensorTypeRepository : BaseRepository<SensorType>, ISensorTypeRepository
    {
        private string command = null;

        public SensorTypeRepository(IConfiguration config)
            : base(config)
        {
        }

        public async Task<List<SensorType>> ObterTodos()
        {
            command = "SELECT * FROM \"TCC_COMP\".\"SensorType\"";

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var sensorTypes = await connection.QueryAsync<SensorType>(command);

                    return sensorTypes.ToList();
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a timeout", GetType().FullName), ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL exception", GetType().FullName), ex);
            }
        }

        public async Task<SensorType> ObterPorId(int sensor_type_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"SensorType\" WHERE sensor_type_id = @sensor_type_id";

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var sensorType = await connection.QueryAsync<SensorType>(command, new { sensor_type_id = sensor_type_id });

                    return sensorType.FirstOrDefault();
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a timeout", GetType().FullName), ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL exception", GetType().FullName), ex);
            }
        }

        public async Task<bool> Adicionar(SensorType newSensorType)
        {
            var dynamicParameters = new DynamicParameters(new
            {
                newSensorType.Tipo,
                newSensorType.Unitofmeasurement,
                newSensorType.Created_at,
            });

            command = "INSERT INTO \"TCC_COMP\".\"SensorType\" (tipo, unitofmeasurement, created_at, active) VALUES (@tipo, @unitofmeasurement, @created_at, true)";

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var retorno = await connection.ExecuteAsync(command, dynamicParameters);

                    if (retorno != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Atualizar(SensorType entidade)
        {
            var dynamicParameters = new DynamicParameters(new
            {
                entidade.Tipo,
                entidade.Unitofmeasurement,
                entidade.Updated_at,
                entidade.Sensor_type_id,
            });

            bool retorno = false;

            command = "UPDATE \"TCC_COMP\".\"SensorType\" SET tipo = @tipo, unitofmeasurement = @unitofmeasurement, updated_at = @updated_at WHERE sensor_type_id = @sensor_type_id";
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var retornoQuery = await connection.ExecuteAsync(command, dynamicParameters);

                    if (retornoQuery != 0)
                    {
                        retorno = true;
                    }
                }

                return retorno;
            }
            catch (Exception)
            {
                return retorno;
            }
        }

        public async Task<bool> Deletar(int sensor_type_id)
        {
            bool retorno = false;

            command = "UPDATE \"TCC_COMP\".\"SensorType\" SET active = false WHERE sensor_type_id = @sensor_type_id";

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var retornoDelete = await connection.ExecuteAsync(command, new { sensor_type_id = sensor_type_id });

                    if (retornoDelete != 0)
                    {
                        retorno = true;
                    }

                    return retorno;
                }
            }
            catch (Exception)
            {
                return retorno;
            }
        }
    }
}
