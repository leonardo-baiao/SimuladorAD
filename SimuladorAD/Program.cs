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
            if(args.Length == 0 || int.TryParse(args[0], out input)){
                System.Console.WriteLine("Please enter 0 for FCFS or 1 for LCFS");
                return;
            }

            cons int MAX = 3200;
            var simulador = new Simulador(input = 0 ? TipoFila.FCFS : TipoFila.LCFS);

            while (simulador.Rodada <= MAX)
            {
                simulador.ProcessaEventos(100);
                simulador.ProximaRodada();
            }
            simulador.GeraEstatisticas();
        }

    }
}
