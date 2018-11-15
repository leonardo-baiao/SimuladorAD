using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Estruturas;

namespace Simulador
{
    class Program
    {

        static void Main(string[] args)
        {
            string input;
            int number;
            double taxa;

            while (true)
            {
                Console.WriteLine("Escolha 0 para FCFS ou 1 para LCFS");
                input = Console.ReadLine();
                
                if (!int.TryParse(input, out number) || number > 1 || number < 0)
                {
                    Console.WriteLine("Por favor insira 0 para FCFS ou 1 para LCFS");
                    continue;
                }
                break;
            }

            var tipoFila = number == 0 ? TipoFila.FCFS : TipoFila.LCFS;

            while (true)
            {
                Console.WriteLine("Escolha a taxa de utilização do sistema (entre 0 e 1)");
                input = Console.ReadLine();

                if(!double.TryParse(input,out taxa) || taxa > 1 || taxa < 0)
                {
                    Console.WriteLine("Por favor insira um número entre 0 e 1. Ex: 0.9");
                    continue;
                }
                break;
            }

            Console.WriteLine("----------------------------Configurações-----------------------------");
            Console.WriteLine("Tipo de fila: " + tipoFila.ToString());
            Console.WriteLine("Kmin: " + Constantes.KMIN);
            Console.WriteLine("Rodadas: " + Constantes.MAX_RODADAS);
            Console.WriteLine("Taxa de Chegada: " + taxa + ", Taxa de atendimento: " + Constantes.TAXA_SERVIDOR);
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Pressione uma tecla para iniciar a simulação");
            Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("Rodada transiente");


            var simulador = new Simulador(tipoFila, taxa);
            
            while (simulador.Rodada <= Constantes.MAX_RODADAS)
            {
                simulador.ProcessaEventos();
                simulador.ProximaRodada();
            }
            simulador.GeraEstatisticas();
            Console.ReadKey();
        }

    }
}
