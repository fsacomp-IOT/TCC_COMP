using TCC_COMP.DOMAIN.Telegram.Receive_Messages;

namespace TCC_COMP.DOMAIN.Telegram.Send_Return
{
    public class SendResult
    {
        public double message_id { get; set; }
        public SendFrom from { get; set; }
        public SendChat chat { get; set; }
        public double date { get; set; }
        public string text { get; set; }
    }
}
