using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public class FilaFCFS : Fila
    {

        public override void AdicionaCliente(Cliente cliente)
        {
            fila.Add(cliente);
        }

        public override void DeletaCliente()
        {
        }

        public override void MostraFila()
        {
            throw new NotImplementedException();
        }

        public override Cliente RetornaCliente()
        {
            var cliente = fila[0];
            fila.RemoveAt(0);
            return cliente;
        }
    }
}
