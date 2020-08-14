namespace TCC_COMP.INFRA.DATA.Repository
{
    using Dapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;

    public class PlantRepository : BaseRepository<Plant>, IPlantRepository
    {
        public PlantRepository(IConfiguration config) : base(config)
        {
        }

        private string command = string.Empty;

        public async Task<List<Plant>> ObterTodosDetalhado()
        {
            command = "SELECT id, air_humidity, air_temperature, name, soil_humidity, solar_light FROM \"TCC_COMP\".\"Plants\"";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(); // Abre uma conexão a assíncrona com o banco de dados

                    var retorno = await connection.QueryAsync<Plant>(command);

                    return retorno.ToList();
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
        }

        public async Task<List<Plant>> ObterTodosSimples()
        {
            command = "SELECT id, name FROM \"TCC_COMP\".\"Plants\"";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<Plant>(command);

                    return retorno.ToList();
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
        }

        public async Task<Plant> ObterPlanta(int id)
        {
            command = "SELECT air_humidity, air_temperature, id, name, soil_humidity, solar_light FROM \"TCC_COMP\".\"Plants\" WHERE id = @id";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {

                    await connection.OpenAsync();

                    var retorno = await connection.QueryAsync<Plant>(command, new { id = id });

                    return retorno.FirstOrDefault();

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
        }

        public async Task<bool> InserirPlanta(Plant plant)
        {
            DynamicParameters dynamicParameters = new DynamicParameters(new
            {
                plant.air_humidity,
                plant.air_temperature,
                plant.name,
                plant.soil_humidity,
                plant.solar_light
            });

            bool retorno = false;

            command = "INSERT INTO \"TCC_COMP\".\"Plants\" (air_humidity, air_temperature, name, soil_humidity, solar_light) VALUES (@air_humidity, @air_temperature, @name, @soil_humidity, @solar_light)";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = connection.BeginTransaction())
                    {
                        var retornoInsert = await connection.ExecuteAsync(command, dynamicParameters, trans);

                        trans.Commit();

                        if (retornoInsert != 0)
                        {
                            retorno = true;
                        }

                        return retorno;
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
        }

        public async Task<bool> AtualizarPlanta(Plant plant)
        {
            DynamicParameters dynamicParameters = new DynamicParameters(new
            {
                plant.air_humidity,
                plant.air_temperature,
                plant.name,
                plant.soil_humidity,
                plant.solar_light,
                plant.id
            });

            bool retorno = false;

            command = "UPDATE \"TCC_COMP\".\"Plants\" SET air_humidity = @air_humidity, air_temperature = @air_temperature, name  = @name, soil_humidity  = @soil_humidity, solar_light = @solar_light WHERE id  = @id";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    
                    using (var trans = connection.BeginTransaction())
                    {
                        var retornoUpdate = await connection.ExecuteAsync(command, dynamicParameters, trans);

                        trans.Commit();

                        if (retornoUpdate != 0)
                        {
                            retorno = true;
                        }

                        return retorno;
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
        }

        public async Task<bool> RemoverPlanta(int id)
        {
            bool retorno = false;

            command = "DELETE FROM \"TCC_COMP\".\"Plants\" WHERE id  = @id";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = connection.BeginTransaction())
                    {
                        var retornoUpdate = await connection.ExecuteAsync(command, new { id = id}, trans);

                        trans.Commit();

                        if (retornoUpdate != 0)
                        {
                            retorno = true;
                        }

                        return retorno;
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
        }
    }
}
