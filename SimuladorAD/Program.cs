using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Estruturas;

namespace Simulador
{
    class Program
    {
        static int input;

        static void Main(string[] args)
        {
            //if(args.Length == 0 || int.TryParse(args[0], out input)){
            //    Console.WriteLine("Por favor insira 0 para FCFS ou 1 para LCFS");
            //    return;
            //}

            //var simulador = new Simulador(input == 0 ? TipoFila.FCFS : TipoFila.LCFS);
            var simulador = new Simulador(TipoFila.FCFS);
            
            while (simulador.Rodada <= Constantes.MAX_RODADAS)
            {
                Console.WriteLine("Iniciando processamento de eventos da rodada " + simulador.Rodada);
                simulador.ProcessaEventos();

                Console.WriteLine("Fim da rodada "  + simulador.Rodada);
                simulador.ProximaRodada();
                Thread.Sleep(2000);
            }
            //simulador.GeraEstatisticas();
        }

    }
}
