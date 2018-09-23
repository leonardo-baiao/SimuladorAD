using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public abstract class EstruturaEventos
    {
        public List<Evento> listaEventos;

        public EstruturaEventos()
        {
            listaEventos = new List<Evento>();
        }

        public abstract void MostraEventos();
        public abstract void AdicionaEvento(Evento evento);
        public abstract Evento RetornaEvento();
        public abstract void DeletaEvento();
    }
}
