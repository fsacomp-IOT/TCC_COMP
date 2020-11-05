using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_COMP.DOMAIN.Telegram
{
    public class NewMessage
    {
        public int chat_id { get; set; }
        public string text { get; set; }
    }
}
