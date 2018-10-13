using System;
using System.Collections.Generic;
using System.Text;

namespace Estatisticas
{
    public class GeradorEstatisticas
    {
        int cons NORMAL = 1.96;

        GeradorPlanilhas geradorPlanilhas;

        public GeradorEstatisticas()
        {
            geradorPlanilhas = new GeradorPlanilhas();
        }

        public void SalvaEstatisticas(List<Estatisticas> estatisticas)
        {
            geradorPlanilhas.Exportar(estatisticas);
        }

        public double CalculaPrecisao(ref IntervaloConfianca ic){
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
                    CalculaICChiQuadrado(media,variancia, n, ref resultado);
                    break;
                default:
                    Console.WriteLine("Variavel aleatória nula");
                    break;
            }
            return resultado;
        }


        private void CalculaICChiQuadrado(double variancia, int n, ref IntervaloConfianca ic)
        {
            ic.U = ((n-1)*variancia)/NORMAL;
            ic.L = -ic.U;
        }

        private void CalculaICTStudent(double media, double variancia, int n, ref IntervaloConfianca ic)
        {
            ic.U = media + NORMAL*(variancia/n);
            ic.L = media - NORMAL*(variancia/n);
        }

        private double CalculaExponencial(double taxa)
        {
            var amostra = GeraAmostra();
            return Math.Log(amostra)/(-taxa);
        }

        private double GeraAmostra()
        {
            var gerador = new Random();
            return gerador.NextDouble();
        }

        public void CalculaSomaAmostras(ref double somAmostras, ref double somQAmostras, double x)
        {
            somAmostras += x;
            somQAmostras += Math.Pow(x,2);
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
