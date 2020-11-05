namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Telegram;

    public interface ITelegramRepository
    {
        Task sendMessageAsync(NewMessage newMessage);
        Task<int> getChatId(string device_id);
    }
}
