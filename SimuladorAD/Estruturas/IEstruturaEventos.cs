using System;
using System.Collections.Generic;
using System.Text;

namespace Simulador.Estruturas
{
    public interface IEstruturaEventos
    {
        
        void MostraEventos();
        void AdicionaEvento(Evento evento);
        Evento RetornaEvento();
        void DeletaEvento();
    }
}
