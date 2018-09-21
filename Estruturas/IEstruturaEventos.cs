using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public interface IEstruturaEventos
    {
        
        void MostraEventos();
        void AdicionaEvento(Evento evento);
        Evento RetornaEvento();
        void DeletaEvento();
    }
}
