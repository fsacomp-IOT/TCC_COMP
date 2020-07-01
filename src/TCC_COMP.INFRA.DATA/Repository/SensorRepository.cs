namespace TCC_COMP.INFRA.DATA.Repository
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.SERVICE.Interfaces.Repository;

    public class SensorRepository : BaseRepository<Sensor>, ISensorRepository
    {
        private string command = string.Empty;

        public SensorRepository(IConfiguration config)
                                : base(config)
        {
        }

        public async Task<List<Sensor>> ObterTodos()
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Sensor\"";

            using(var connection = new NpgsqlConnection(this.ConnectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var retorno = await connection.QueryAsync<Sensor>(this.command);
                    return retorno.ToList();
                }
                catch (Exception e)
                {
                    return new List<Sensor>();
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

        public async Task<Sensor> ObterPorId(Guid id)
        {
            command = "SELECT * FROM \"TCC_COMP\".\"Sensor\" WHERE sensor_id = @sensor_id";

            Sensor saida = new Sensor();

            using(var connection = new NpgsqlConnection(this.ConnectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var retorno = await connection.QueryAsync<Sensor>(this.command, new { sensor_id = id});

                    return retorno.FirstOrDefault();

                }
                catch (Exception e)
                {
                    //throw e;
                    return new Sensor();
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

        public async Task<bool> Adicionar(Sensor newSensor)
        {
            bool retorno = false;

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.AddDynamicParams(new {
                newSensor.Sensor_Id,
                newSensor.Sensor_Name,
                newSensor.Created_At,
                newSensor.Device,
                newSensor.Sensor_Type
            });

            command = "INSERT INTO \"TCC_COMP\".\"Sensor\" (sensor_id, sensor_name, created_at, device, sensor_type) VALUES (@Sensor_Id, @Sensor_Name, @Created_At, @Device, @Sensor_Type)";        
            
            using(var connection = new NpgsqlConnection(this.ConnectionString))
            {
                await connection.OpenAsync();
                
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        var retornoInsert = await connection.ExecuteAsync(command, dynamicParameters);

                        trans.Commit();

                        if(retornoInsert != 0)
                        {
                            retorno = true;
                        }

                        return retorno;
                    }
                    catch(Exception)
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

        public async Task<bool> Atualizar(Sensor AlteracaoSensor)
        {
            bool retorno = false;

            var dynamicParameters = new DynamicParameters(new {
                AlteracaoSensor.Sensor_Name,
                AlteracaoSensor.Update_At,
                AlteracaoSensor.Sensor_Id
            });

            command = "UPDATE \"TCC_COMP\".\"Sensor\" SET sensor_name = @sensor_name, updated_at = @updated_at WHERE sensor_id = @sensor_id";
        
            
            using(var connection = new NpgsqlConnection(this.ConnectionString))
            {
                
                await connection.OpenAsync();

                using(var trans = connection.BeginTransaction()){
                    
                    try
                    {
                        var retornoUpdate = await connection.ExecuteAsync(command, dynamicParameters);

                        trans.Commit();

                        if(retornoUpdate != 0)
                        {
                            retorno = true;
                        }

                        return retorno;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return retorno;
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