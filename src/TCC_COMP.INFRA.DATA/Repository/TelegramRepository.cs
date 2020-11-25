namespace TCC_COMP.INFRA.DATA.Repository
{
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Npgsql;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;
    using TCC_COMP.DOMAIN.Telegram;
    using TCC_COMP.DOMAIN.Telegram.Send_Return;
    using TCC_COMP.SERVICE.Interfaces.Repository;

    public class TelegramRepository : BaseRepository<TelegramSend>, ITelegramRepository
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IOptions<Keys> _appSettings;

        public TelegramRepository(IOptions<Keys> appSettings,
                                  IConfiguration config)
                                 : base(config)
        {
            _appSettings = appSettings;
        }

        public async Task sendMessageAsync(NewMessage message)
        {
            TelegramSend ret = new TelegramSend();

            client.DefaultRequestHeaders.Accept.Clear();

            string url = "https://api.telegram.org/" + _appSettings.Value.BOT_API + "/sendMessage";


            var dataAsString = JsonConvert.SerializeObject(message);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                ret = JsonConvert.DeserializeObject<TelegramSend>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<int> getChatId(string device_id)
        {
            string command = "SELECT chat_id FROM \"TCC_COMP\".\"DeviceChat\" WHERE device_id = @device_id";


            using (var conn = new NpgsqlConnection(this.ConnectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    var ret = await conn.QueryAsync<int>(command, new { device_id });
                    if (ret.Count() > 0)
                        return ret.First();
                    else
                        return 0;
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

        public async Task<bool> DeleteRelation(string device_id)
        {
            string command = "DELETE FROM \"TCC_COMP\".\"DeviceChat\" WHERE device_id = @device_id";

            bool response = false;

            using (var conn = new NpgsqlConnection(this.ConnectionString))
            {
                
                try
                {
                    await conn.OpenAsync();

                    using (var trans = await conn.BeginTransactionAsync())
                    {
                        var ret = await conn.ExecuteAsync(command, new { device_id });
                        if (ret > 0)
                        {
                            response = true;
                            await trans.CommitAsync();
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

            return response;
        }
    }
}