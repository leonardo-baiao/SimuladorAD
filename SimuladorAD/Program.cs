using System;
using System.Collections.Generic;
using System.Text;
using Estruturas;

namespace Simulador
{
    class Program
    {
        static void Main(string[] args)
        {
            var processando = true;
            var simulador = new Simulador(TipoEstrutura.FILA);

            while (processando)
            {
                simulador.ProximoEvento();
                simulador.TrataEvento();
                simulador.GeraEstatisticas();
            }
        }

    }
}
