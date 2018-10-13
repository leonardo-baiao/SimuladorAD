using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public class FilaLCFS : Fila
    {

        public override void AdicionaCliente(Cliente cliente)
        {
            fila.Add(cliente);
        }

        public override void DeletaCliente()
        {
            fila.RemoveAt(fila.Count - 1);
        }

        public override void MostraFila()
        {
            throw new NotImplementedException();
        }

        public override Cliente RetornaCliente()
        {
            var cliente = fila[fila.Count - 1];
            fila.RemoveAt(fila.Count - 1).
            return cliente;
        }

    }
}
