using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public class PilhaEventos : EstruturaEventos
    {

        public override void AdicionaEvento(Evento evento)
        {
            listaEventos.Add(evento);
        }

        public override void DeletaEvento()
        {
            listaEventos.RemoveAt(listaEventos.Count - 1);
        }

        public override void MostraEventos()
        {
            throw new NotImplementedException();
        }

        public override Evento RetornaEvento()
        {
            return listaEventos[listaEventos.Count - 1];
        }

    }
}
