using System;
using System.Collections.Generic;
using System.Linq;

namespace Estruturas
{
    public class ListaEventos
    {
        private List<Evento> listaEventos;

        public ListaEventos()
        {
            listaEventos = new List<Evento>();
        }

        public Evento ProximoEvento()
        {
            try
            {
                var prox = listaEventos[0];
                RemoveEvento();
                return prox;
            }
            catch { return null; }
        }

        public void AdicionaEvento(Evento evento)
        {
            try
            {
                listaEventos.Insert(listaEventos.FindIndex(e => e.Tempo > evento.Tempo), evento);
            }
            catch(Exception) { listaEventos.Add(evento); }
        }

        public void RemoveEvento()
        {
            listaEventos.RemoveAt(0);
        }

    }
}
