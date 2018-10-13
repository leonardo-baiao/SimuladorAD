using System;

namespace Estruturas
{
    class ListaEventos
    {
        private List<Evento> listaEventos;

        public ListaEventos()
        {
            listaEventos = new List<Evento>();
        }

        public Evento ProximoEvento()
        {
            var prox = listaEventos[0];
            RemoveEvento();
            return prox;
        }

        public void AdicionaEvento(Evento evento)
        {
            listaEventos.Add(evento);
        }

        public void RemoveEvento()
        {
            listaEventos.RemoveAt(0);
        }

    }
}
