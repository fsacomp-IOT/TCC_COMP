namespace TCC_COMP.INFRA.DATA.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
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

        #region [SELECT]

        /// <summary>
        /// Método para obter todos os Devices ativos.
        /// </summary>
        /// <returns>List<Device></Device>.</returns>
        public async Task<List<Device>> ObterTodos()
        {
            command = "SELECT Device.id, Device.name, Device.created_at, Device.updated_at FROM \"TCC_COMP\".\"Device\" AS Device ";

            using (var connection = new NpgsqlConnection(this.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(); // Abre uma conexão a assíncrona com o banco de dados

                    var retorno = await connection.QueryAsync<Device>(this.command);

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

        public async Task<string> ObterRelacaoPlanta(string device_id)
        {
            command = "SELECT Relacao.plant_id FROM \"TCC_COMP\".\"DevicePlants\" AS Relacao WHERE device_id = @device_id";

            using(var connection = new NpgsqlConnection(this.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<string>(this.command, new { device_id = device_id});

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

        public async Task<Device> ObterPorId(string device_id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Device\" WHERE id = @device_id";

            using (var connection = new NpgsqlConnection(this.ConnectionString))
            {
                try
                {

                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<Device>(this.command, new { device_id = device_id });

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

            command = "INSERT INTO \"TCC_COMP\".\"Device\"(id, name, created_at, updated_at) VALUES (@id, @name, @created_at, @updated_at)";

            using (var connection = new NpgsqlConnection(this.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = connection.BeginTransaction())
                    {
                        var retornoQuery = await connection.ExecuteAsync(this.command, dynamicParameters, trans);

                        if (retornoQuery != 0)
                        {
                            await trans.CommitAsync();
                            retorno = true;
                        }
                    }

                    return retorno;
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

        public async Task<bool> IncluirRelacaoPlanta(string device_id, string plant)
        {
            bool retorno = false;

            int plant_id = Convert.ToInt32(plant);

            DynamicParameters dynamicParameters = new DynamicParameters(new { 
                device_id,
                plant_id
            });

            command = "INSERT INTO \"TCC_COMP\".\"DevicePlants\"(device_id, plant_id) VALUES (@device_id, @plant_id)";

            using (var connection = new NpgsqlConnection(this.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = connection.BeginTransaction())
                    {
                        var retornoQuery = await connection.ExecuteAsync(command, dynamicParameters);

                        if (retornoQuery != 0)
                        {
                            await trans.CommitAsync();
                            retorno = true;
                        }
                    }

                }
                catch(TimeoutException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu um timeout", GetType().FullName), ex);
                }
                catch (NpgsqlException ex)
                {
                    throw new Exception(string.Format("{0}.WithConnection() ocorreu uma exceção SQL Mensagem: {1}", GetType().FullName, ex.Message), ex);
                }
            }

            return retorno;
        }

        #endregion

        #region [UPDATE]

        public async Task<bool> Atualizar(Device updateDevice)
        {
            bool retorno = false;

            var dynamicParamenters = new DynamicParameters(new
            {
                updateDevice.name,
                updateDevice.updated_at,
                updateDevice.id
            });

            command = "UPDATE \"TCC_COMP\".\"Device\" SET name = @name, updated_at = @updated_at WHERE id = @id";

            using (var connection = new NpgsqlConnection(this.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = connection.BeginTransaction())
                    {
                        var retornoQuery = await connection.ExecuteAsync(this.command, dynamicParamenters);

                        await trans.CommitAsync();

                        retorno = true;
                    }

                    return retorno;
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

        public async Task<bool> AtualizarRelacaoPlanta(string device_id, string plant_id)
        {
            bool retorno = false;

            int planta = Convert.ToInt32(plant_id);

            DynamicParameters dynamicParameters = new DynamicParameters(new { 
                device_id, 
                plant_id
            });

            command = "UPDATE \"TCC_COMP\".\"DevicePlants\" SET plant_id = @plant_id WHERE device_id = @device_id";

            using (var connection = new NpgsqlConnection(this.ConnectionString)) 
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = await connection.BeginTransactionAsync())
                    {
                        var retornoQuery = await connection.ExecuteAsync(command, dynamicParameters);

                        if(retornoQuery != 0)
                        {
                            await trans.CommitAsync();

                            retorno = true;
                        }

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

            return retorno;
        }

        #endregion

        #region [DELETE]

        public async Task<bool> Deletar(string device_id)
        {
            bool retorno = false;

            command = "DELETE FROM \"TCC_COMP\".\"Device\" WHERE id = @id";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = connection.BeginTransaction())
                    {
                        var retornoQuery = await connection.ExecuteAsync(command, new { id = device_id });

                        retorno = true;

                        await trans.CommitAsync();
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

            return retorno;
        }

        #endregion
    }
}
