using System;
using System.Collections.Generic;
using System.Text;

namespace Estatisticas
{
    public class GeradorEstatisticas
    {
        double amostra;
        GeradorPlanilhas geradorPlanilhas;

        public GeradorEstatisticas()
        {
            geradorPlanilhas = new GeradorPlanilhas();
        }
        
        public void SalvaEstatisticas()
        {
            geradorPlanilhas.Exportar();
        }

        public void CalculaIC()
        {

        }
        
        public void CalculaPoisson(double taxa)
        {
            amostra = GeraAmostra();
            return;
        }

        public double CalculaExponencial(double taxa)
        {
            amostra = GeraAmostra();
            return Math.Log(amostra)/(-taxa);
        }

        public void CalculaChiQuadrado()
        {

        }

        public void CalculaTStudent()
        {

        }

        private double GeraAmostra()
        {
            var gerador = new Random();
            return gerador.NextDouble();
        }
    }
}
