using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    class PilhaEventos : IEstruturaEventos
    {
        private readonly List<Evento> listaEventos;

        public PilhaEventos()
        {
            listaEventos = new List<Evento>(); 
        }

        public void AdicionaEvento(Evento evento)
        {
            listaEventos.Add(evento);
        }

        public void MostraEventos()
        {
            throw new NotImplementedException();
        }

        public Evento RetornaEvento()
        {
            return listaEventos[listaEventos.Count - 1];
        }
        public void DeletaEvento()
        {
            listaEventos.RemoveAt(listaEventos.Count - 1);
        }

    }
}
