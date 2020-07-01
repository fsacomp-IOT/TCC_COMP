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

    /// <summary>
    /// Repositorio que contém o CRUD de Devices.
    /// </summary>
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        private string command = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceRepository"/> class.
        /// </summary>
        /// <param name="config">Injeção de Dependência utilizada para obter a ConnectionString.</param>
        public DeviceRepository(IConfiguration config)
            : base(config)
        {
        }

        /// <summary>
        /// Método para obter todos os Devices ativos.
        /// </summary>
        /// <returns>List<Device></Device>.</returns>
        public async Task<List<Device>> ObterTodos()
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device\" WHERE connected = true";

            try
            {
                using (var connection = new NpgsqlConnection(this.ConnectionString))
                {
                    await connection.OpenAsync(); // Abre uma conexão a assíncrona com o banco de dados

                    var retorno = await connection.QueryAsync<Device>(this.command);

                    return retorno.ToList();
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a timeout", this.GetType().FullName), ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(string.Format("{0}.WithConnection() experienced a SQL exception", this.GetType().FullName), ex);
            }
        }

        public async Task<Device> ObterPorId(Guid device_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device\" WHERE device_id = @device_id";

            try
            {
                using (var connection = new NpgsqlConnection(this.ConnectionString))
                {
                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<Device>(this.command, new { device_id = device_id });

                    return retorno.FirstOrDefault();
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

        /// <summary>
        /// Query para inserção de novos Devices.
        /// </summary>
        /// <param name="newDevice">Novo Dispositivo.</param>
        /// <returns>string.</returns>
        public async Task<bool> Adicionar(Device newDevice)
        {
            bool retorno = false;

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.AddDynamicParams(new
            {
                newDevice.Device_Id,
                newDevice.Device_Name,
                newDevice.Connected,
                newDevice.Created_At,
            });

            this.command = "INSERT INTO \"TCC_COMP\".\"Device\"(device_id, device_name, connected, created_at) VALUES (@device_id, @device_name, @connected, @created_at)";

            try
            {
                using (var connection = new NpgsqlConnection(this.ConnectionString))
                {
                    await connection.OpenAsync();

                    var retornoQuery = await connection.ExecuteAsync(this.command, dynamicParameters);

                    if (retornoQuery != 0)
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

        public async Task<bool> Atualizar(Device alteracaoDevice)
        {
            bool retorno = false;

            var dynamicParamenters = new DynamicParameters(new
            {
                alteracaoDevice.Device_Name,
                alteracaoDevice.Updated_At,
                alteracaoDevice.Device_Id,
            });

            this.command = "UPDATE \"TCC_COMP\".\"Device\" SET device_name = @Device_name, connected = true, updated_at = @Updated_at WHERE device_id = @Device_id";

            try
            {
                using (var connection = new NpgsqlConnection(this.ConnectionString))
                {
                    await connection.OpenAsync();

                    var retornoQuery = await connection.ExecuteAsync(this.command, dynamicParamenters);

                    retorno = true;
                }
            }
            catch (TimeoutException)
            {
                retorno = false;
            }
            catch (NpgsqlException)
            {
                retorno = false;
            }

            return retorno;
        }

        public async Task<bool> Deletar(Guid device_id)
        {
            bool retorno = false;

            this.command = "UPDATE \"TCC_COMP\".\"Device\" SET connected = false WHERE device_id = @device_id";

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var retornoQuery = await connection.ExecuteAsync(command, new { device_id = device_id });

                    retorno = true;
                }
            }
            catch (TimeoutException)
            {
                retorno = false;
            }
            catch (NpgsqlException)
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
