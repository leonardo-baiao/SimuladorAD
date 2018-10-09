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
        
        public void SalvaEstatisticas(List<Estatisticas> estatisticas)
        {
            geradorPlanilhas.Exportar(estatisticas);
        }

        public double CalculaPrecisao(IntervaloConfianca ic){
            return (ic.L - ic.U)/(ic.L + ic.U);
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
            var va = CalculaChiQuadrado(n);
            ic.U = ((n-1)*variancia)/va;
            ic.L = -ic.U;
        }

        private void CalculaICTStudent(double media, double variancia, int n, ref IntervaloConfianca ic)
        {
            var va = CalculaTStudent(n);
            ic.U = media + va*(variancia/n);
            ic.L = media - va*(variancia/n);
        }

        private double CalculaChiQuadrado(int n)
        {
            
        }

        private double CalculaTStudent(int n)
        {

        }

        private double CalculaPoisson(double taxa)
        {
            amostra = GeraAmostra();
            return;
        }

        private double CalculaExponencial(double taxa)
        {
            amostra = GeraAmostra();
            return Math.Log(amostra)/(-taxa);
        }
        private double GeraAmostra()
        {
            var gerador = new Random();
            return gerador.NextDouble();
        }
    }
}
