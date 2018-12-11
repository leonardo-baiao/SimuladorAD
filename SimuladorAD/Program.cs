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
            double taxaChegada;

            while (true)
            {
                Console.WriteLine("Escolha 0 para FCFS ou 1 para LCFS");
                input = Console.ReadLine();
                
                if (!int.TryParse(input, out number) || number > 1 || number < 0)
                {
                    continue;
                }
                break;
            }

            var tipoFila = number == 0 ? TipoFila.FCFS : TipoFila.LCFS;

            while (true)
            {
                Console.WriteLine("Escolha a taxa de utilização do sistema (entre 0.1 e 0.9 para especifica e 1 para percorrer todas)");
                input = Console.ReadLine();

                if(!double.TryParse(input,out taxaChegada) || taxaChegada > 1 || taxaChegada < 0)
                {
                    continue;
                }
                break;
            }

            if (taxaChegada == 1)
            {
                foreach (var taxa in Constantes.taxas)
                    IniciarSimulacao(tipoFila, taxa);
            }
            else
                IniciarSimulacao(tipoFila, taxaChegada);
        }


        private static void IniciarSimulacao(TipoFila tipoFila, double taxa)
        {
            Console.WriteLine("----------------------------Configurações-----------------------------\n");
            Console.WriteLine("Tipo de fila: " + tipoFila.ToString());
            Console.WriteLine("Kmin: " + Constantes.KMIN);
            Console.WriteLine("Rodadas: " + Constantes.MAX_RODADAS);
            Console.WriteLine("Taxa de Chegada: " + taxa + ", Taxa de atendimento: " + Constantes.TAXA_SERVIDOR);
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Pressione uma tecla para iniciar a simulação");
            Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("Iniciando Simulação");


            var simulador = new Simulador(tipoFila, taxa);

            var start = DateTime.Now;
            
            simulador.ProcessaRodadaTransiente();

            while (simulador.Rodada <= Constantes.MAX_RODADAS)
            {

                simulador.ProcessaEventos();
                simulador.CalculaEstatisticas();
                simulador.ProximaRodada();
            }
            simulador.CalculaEstatisticasFinais();

            var time = DateTime.Now.Subtract(start);
            Console.WriteLine(time);
            Console.ReadKey();
        }

    }
}
