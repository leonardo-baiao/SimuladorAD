using System;

namespace Simulador
{
    class Simulador
    {
        static void Main(string[] args)
        {
            bool processando = true;

            while (processando)
            {
                ProximoEvento();
                TrataEvento();
                GeraEstatisticas();
            }
        }
    }
}
