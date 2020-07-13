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
            command = "SELECT * FROM \"TCC_COMP\".\"Device\"";

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

        public async Task<Device> ObterPorId(string device_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device\" WHERE id = @device_id";

            try
            {
                using (var connection = new NpgsqlConnection(this.ConnectionString))
                {
                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<Device>(this.command, new { id = device_id });

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
                newDevice.id,
                newDevice.name,
                newDevice.created_at,
                newDevice.updated_at
            });

            this.command = "INSERT INTO \"TCC_COMP\".\"Device\"(id, name, created_at, updated_at) VALUES (@id, @name, @created_at, @updated_at)";

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

        public async Task<bool> Atualizar(string device_id, Device updateDevice)
        {
            bool retorno = false;

            var dynamicParamenters = new DynamicParameters(new
            {
                updateDevice.name,
                updateDevice.updated_at,
                device_id
            });

            this.command = "UPDATE \"TCC_COMP\".\"Device\" SET name = @name, updated_at = @updated_at WHERE id = @device_id";

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

        public async Task<bool> Deletar(string device_id)
        {
            bool retorno = false;

            this.command = "DELETE FROM \"TCC_COMP\".\"Device\" WHERE id = @id";

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var retornoQuery = await connection.ExecuteAsync(command, new { id = device_id });

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
