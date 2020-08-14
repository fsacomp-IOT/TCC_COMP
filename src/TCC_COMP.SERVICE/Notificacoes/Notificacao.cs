using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_COMP.SERVICE.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; set; }
    }
}
