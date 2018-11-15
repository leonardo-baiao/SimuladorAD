using Estruturas;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estatisticas
{
    public class GeradorEstatisticas
    {
        private Random gerador;
        private GeradorPlanilhas geradorPlanilhas;

        public GeradorEstatisticas()
        {
            geradorPlanilhas = new GeradorPlanilhas();
            gerador = new Random();
        }

        public void SalvaEstatisticas(List<Estatistica> estatisticas)
        {
            geradorPlanilhas.Exportar(estatisticas);
        }

        private void CalculaPrecisao(ref IntervaloConfianca ic)
        {
             ic.Precisao = (ic.U - ic.L)/(ic.U + ic.L);
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
            ChiSquared cs = new ChiSquared(n-1);
            Console.WriteLine("0.025: " + cs.CumulativeDistribution(0.025));
            Console.WriteLine("0.975: " + cs.CumulativeDistribution(0.975));
            ic.U = (Constantes.KMIN * (n - 1) * variancia) / cs.CumulativeDistribution(0.025);
            ic.L = (Constantes.KMIN * (n - 1) * variancia) / cs.CumulativeDistribution(0.975);
        }

        private void CalculaICTStudent(double media, double variancia, int n, ref IntervaloConfianca ic)
        {
            StudentT ts = new StudentT(0,1,n-1);
            var percentil = ts.CumulativeDistribution(0.975);

            ic.U = media + percentil * (variancia/n);
            ic.L = media - percentil * (variancia/n);
        }

        public double CalculaExponencial(double taxa)
        {
            var amostra = GeraAmostra();
            return Math.Log(amostra)/(-taxa);
        }

        private double GeraAmostra()
        {
            return gerador.NextDouble();
        }

        public void CalculaSomaAmostras(ref Estatistica estatistica, double x)
        {
            estatistica.SomaAmostras += x;
        }

        public double CalculaMediaAmostral(double somAmostras, double n)
        {
            return (somAmostras)/n;
        }

        public double CalculaVarianciaAmostral(double somAmostras, double somQAmostras, double n)
        {
            return (somQAmostras/(n-1)) - (Math.Pow(somAmostras,2)/(n*(n-1)));
        }

    }
}
