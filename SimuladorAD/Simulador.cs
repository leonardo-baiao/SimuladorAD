using System;
using Estatisticas;
using Estruturas;

namespace Simulador
{
    class Simulador
    {
        private EstruturaEventos listaEventos;
        private TipoEstrutura tipoEstrutura;
        private Evento evento;
        private GeradorEstatisticas geradorEstatisticas;

        public Simulador(TipoEstrutura tipoEstrutura)
        {
            GeraFilaEventos(tipoEstrutura);
            geradorEstatisticas = new GeradorEstatisticas();
        }

        private void GeraFilaEventos(TipoEstrutura tipoEstrutura)
        {
            if (tipoEstrutura.Equals(TipoEstrutura.FILA))
                listaEventos = new FilaEventos();
            else
                listaEventos = new PilhaEventos();
        }

        public void GeraEstatisticas()
        {
            geradorEstatisticas.CalculaPoisson();
        }

        public void TrataEvento()
        {
            
        }

        public void ProximoEvento()
        {
            evento = listaEventos.RetornaEvento();
            listaEventos.DeletaEvento();
        }
    }
}
