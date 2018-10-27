using Estruturas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estatisticas
{
    public class GeradorEstatisticas
    {

        GeradorPlanilhas geradorPlanilhas;

        public GeradorEstatisticas()
        {
            geradorPlanilhas = new GeradorPlanilhas();
        }

        public void SalvaEstatisticas(List<Estatistica> estatisticas)
        {
            geradorPlanilhas.Exportar(estatisticas);
        }

        private void CalculaPrecisao(ref IntervaloConfianca ic)
        {
             ic.Precisao = (ic.L - ic.U)/(ic.L + ic.U);
        }

        public IntervaloConfianca CalculaIC(double media, double variancia, VariavelAleatoria va, int n)
        {
            var resultado = new IntervaloConfianca();

            switch (va)
            {
                case VariavelAleatoria.TSTUDENT:
                    CalculaICTStudent(media,variancia, n, ref resultado);
                    break;
                case VariavelAleatoria.CHIQUADRADO:
                    CalculaICChiQuadrado(variancia, n, ref resultado);
                    break;
                default:
                    Console.WriteLine("Variavel aleatória nula");
                    break;
            }

            CalculaPrecisao(ref resultado);

            return resultado;
        }


        private void CalculaICChiQuadrado(double variancia, int n, ref IntervaloConfianca ic)
        {
            ic.U = ((n-1)*variancia)/Constantes.NORMAL;
            ic.L = -ic.U;
        }

        private void CalculaICTStudent(double media, double variancia, int n, ref IntervaloConfianca ic)
        {
            ic.U = media + Constantes.NORMAL *(variancia/n);
            ic.L = media - Constantes.NORMAL *(variancia/n);
        }

        public double CalculaExponencial(double taxa)
        {
            var amostra = GeraAmostra();
            return Math.Log(amostra)/(-taxa);
        }

        private double GeraAmostra()
        {
            var gerador = new Random();
            return gerador.NextDouble();
        }

        public void CalculaSomaAmostras(ref Estatistica estatistica, double x)
        {
            estatistica.SomaAmostras += x;
            estatistica.SomaQAmostras += Math.Pow(x,2);
        }

        public double CalculaMediaAmostral(double somAmostras, int n)
        {
            return (somAmostras)/n;
        }

        public double CalculaVarianciaAmostral(double somAmostras, double somQAmostras, int n)
        {
            return somQAmostras/(n-1) - Math.Pow(somAmostras,2)/n*(n-1);
        }

    }
}
