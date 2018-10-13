using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public abstract class Fila
    {
        public List<Cliente> fila;

        public EstruturaEventos()
        {
            fila = new List<Cliente>();
        }

        public abstract void MostraFila();
        public abstract void AdicionaCliente(Cliente cliente);
        public abstract Cliente RetornaCliente();
    }

    public enum TipoFila
    {
        LCFS = 1,
        FCFS= 2
    }
}
