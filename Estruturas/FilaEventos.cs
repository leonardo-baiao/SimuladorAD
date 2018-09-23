using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public class FilaEventos : EstruturaEventos
    {
        
        public override void AdicionaEvento(Evento evento)
        {
            listaEventos.Add(evento);
        }

        public override void DeletaEvento()
        {
            listaEventos.RemoveAt(0);
        }

        public override void MostraEventos()
        {
            throw new NotImplementedException();
        }

        public override Evento RetornaEvento()
        {
            return listaEventos[0];
        }
    }
}
