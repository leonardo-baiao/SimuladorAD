using System;
using System.Collections.Generic;
using System.Text;

namespace Simulador.Estruturas
{
    public class FilaEventos : IEstruturaEventos
    {
        private readonly List<Evento> listaEventos;

        public FilaEventos()
        {
            listaEventos = new List<Evento>();
        }
        public void AdicionaEvento(Evento evento)
        {
            listaEventos.Add(evento);
        }

        public void DeletaEvento()
        {
            listaEventos.RemoveAt(0);
        }

        public void MostraEventos()
        {
            throw new NotImplementedException();
        }

        public Evento RetornaEvento()
        {
            return listaEventos[0];
        }
    }
}
