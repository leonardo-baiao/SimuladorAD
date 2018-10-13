using System;
using Estatisticas;
using Estruturas;
using System.Collections.Generic;

namespace Simulador
{
    class Simulador
    {
        private int rodada = 0;
        private double tempo = 0;
        private readonly int cons TAXASERVIDOR = 1;
        private readonly int cons TAXACHEGADA = 1;
        private Evento evento;
        private ListaEventos listaEventos;
        private Fila fila;
        private List<Estatistica> listaEstatisticas;
        private readonly GeradorEstatisticas geradorEstatisticas;

        public Simulador(TipoFila tipoFila)
        {
            GeraFila(tipoFila);
            listaEventos = new ListaEventos();
            listaEstatisticas = new List<Estatistica>();
            geradorEstatisticas = new GeradorEstatisticas();
        }

        public int Rodada
        {
            get
            {
                return rodada;
            }
        }

        public void ProcessaEventos(int kmin)
        {
            while(kmin > 0)
            {
                TrataEvento();
            }
        }

        public void GeraEstatisticas()
        {

        }

        private void TrataEvento()
        {
            evento = listaEventos.ProximoEvento();

            if(evento == null)
            {
                filaEventos.AdicionaEvento(CalculaProximaChegada());
                return;
            }
            tempo += evento.Tempo;

            switch(evento.Tipo)
            {
                case TipoEvento.CHEGADA :
                    ChegadaCliente();
                    break;
                case TipoEvento.SAIDA :
                    AtendimentoCliente();
                    break;
                default:
                    break;
            }
        }

        private void ChegadaCliente()
        {
            fila.AdicionaCliente(new Cliente{ Tipo = rodada });
            filaEventos.AdicionaEvento(CalculaProximaChegada());
            filaEventos.AdicionaEvento(CalculaTempoAtendimento());
        }

        private void AtendimentoCliente(Evento evento)
        {
            var cliente = fila.RetornaCliente();
            geradorEstatisticas.CalculaSomaAmostras();
        }

        private void GeraFila(TipoFila tipoFila)
        {
            if (tipoFila.Equals(TipoFila.FCFS))
                fila = new FilaFCFS();
            else
                fila = new FilaLCFS();
        }

        private Evento CalculaTempoAtendimento()
        {
            return new Evento
            {
                Tipo = TipoEvento.SAIDA,
                Tempo = tempo + geradorEstatisticas.CalculaExponencial(TAXASERVIDOR)
            };
        }

        private Evento CalculaProximaChegada()
        {
            return new Evento
            {
                Tipo = TipoEvento.CHEGADA,
                Tempo = tempo + geradorEstatisticas.CalculaExponencial(TAXACHEGADA)
            };
        }
    }
}
