namespace TCC_COMP.SERVICE.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TCC_COMP.SERVICE.Notificacoes;

    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
