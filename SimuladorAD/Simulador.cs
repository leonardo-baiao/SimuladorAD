using System;
using Estatisticas;
using Estruturas;
using System.Collections.Generic;
using System.Threading;

namespace Simulador
{
    class Simulador
    {
        private double tempo = 0;
        private int amostras = 0;
        private bool servidor = false;
        private Evento eventoAtual;
        private ListaEventos listaEventos;
        private Fila fila;
        private Estatistica estatisticaAtual;
        private List<Estatistica> listaEstatisticas;
        private readonly GeradorEstatisticas _geradorEstatisticas;

        public Simulador(TipoFila tipoFila)
        {
            GeraFila(tipoFila);
            listaEventos = new ListaEventos();
            estatisticaAtual = new Estatistica { Rodada = Rodada };
            listaEstatisticas = new List<Estatistica>();
            _geradorEstatisticas = new GeradorEstatisticas();
        }

       
        public int Rodada { get; private set; }


        public void ProcessaEventos()
        {
            while(amostras < Constantes.KMIN)
            {
                TrataEvento();
            }
        }

        public void ProximaRodada()
        {
            GeraRodadaEstatisticas();
            Rodada++;
            estatisticaAtual = new Estatistica { Rodada = Rodada };
            amostras = 0;
        }

        private void GeraRodadaEstatisticas()
        {
            Console.WriteLine("Gerando estatisticas da rodada " + Rodada);

            estatisticaAtual.QuantidadeAmostras = amostras;
            estatisticaAtual.TempoMedio = _geradorEstatisticas.CalculaMediaAmostral(estatisticaAtual.SomaAmostras, amostras);
            estatisticaAtual.TempoQMedio= _geradorEstatisticas.CalculaMediaAmostral(estatisticaAtual.SomaQAmostras, amostras);
            //estatisticaAtual.QuantidadeMedia;
            listaEstatisticas.Add(estatisticaAtual);

            Console.WriteLine("Amostras: " + amostras);
            Console.WriteLine("Tempo Medio: " + estatisticaAtual.TempoMedio);
        }

        public void GeraEstatisticas()
        {
            double tempoMedioFinal;
            double varianciaTempoFinal;
            double somaTempoMedio = 0;
            double somaTempoQMedio = 0;
            IntervaloConfianca ic;


            Console.WriteLine("Gerando estatisticas finais");
            foreach(var estatistica in listaEstatisticas)
            {
                somaTempoMedio += estatistica.TempoMedio;
                somaTempoQMedio += estatistica.TempoQMedio;
            }

            tempoMedioFinal = _geradorEstatisticas.CalculaMediaAmostral(somaTempoMedio,listaEstatisticas.Count);
            varianciaTempoFinal = _geradorEstatisticas.CalculaVarianciaAmostral(somaTempoMedio, somaTempoQMedio, listaEstatisticas.Count);

            ic = _geradorEstatisticas.CalculaIC(tempoMedioFinal, varianciaTempoFinal, VariavelAleatoria.TSTUDENT, listaEstatisticas.Count);
            
            Console.WriteLine("Rodadas: " + listaEstatisticas.Count);
            Console.WriteLine("Tempo Medio: " + tempoMedioFinal);
            Console.WriteLine("Variancia Tempo: " + varianciaTempoFinal);
            Console.WriteLine("Intervalo de Confiança:");
            Console.WriteLine("    L: {0}, U: {1}, P: {2}", ic.L,ic.U,ic.Precisao);
        }

        private void TrataEvento()
        {
            eventoAtual = listaEventos.ProximoEvento();

            if(eventoAtual == null)
            {
                listaEventos.AdicionaEvento(CalculaProximaChegada());
                return;
            }

            tempo = eventoAtual.Tempo;
            Console.WriteLine("Tempo: " + tempo);
            Console.WriteLine("Fila: " + fila.Quantidade);

            switch(eventoAtual.Tipo)
            {
                case TipoEvento.CHEGADA :
                    ChegadaFregues();
                    break;
                case TipoEvento.SAIDA :
                    AtendimentoFregues();
                    break;
                default:
                    break;
            }
        }

        private void ChegadaFregues()
        {
            Console.WriteLine("Processando evento de chegada");
            fila.AdicionaFregues(new Fregues{ Tipo = Rodada , TempoChegada = tempo });

            if (!servidor)
            {
                listaEventos.AdicionaEvento(CalculaTempoAtendimento());
                servidor = true;
            }

            listaEventos.AdicionaEvento(CalculaProximaChegada());
        }

        private void AtendimentoFregues()
        {
            Console.WriteLine("Processando evento de partida");
            var cliente = fila.RetornaFregues();

            _geradorEstatisticas.CalculaSomaAmostras(ref estatisticaAtual, tempo - cliente.TempoChegada);
            amostras++;

            if (fila.Quantidade == 0)
            {
                servidor = false;
                return;
            }

            listaEventos.AdicionaEvento(CalculaTempoAtendimento());
        }


        private Evento CalculaTempoAtendimento()
        {
            return new Evento
            {
                Tipo = TipoEvento.SAIDA,
                Tempo = tempo + _geradorEstatisticas.CalculaExponencial(Constantes.TAXA_SERVIDOR)
            };
        }

        private Evento CalculaProximaChegada()
        {
            return new Evento
            {
                Tipo = TipoEvento.CHEGADA,
                Tempo = tempo + _geradorEstatisticas.CalculaExponencial(Constantes.TAXA_CHEGADA)
            };
        }
        
        private void GeraFila(TipoFila tipoFila)
        {
            if (tipoFila.Equals(TipoFila.FCFS))
                fila = new FilaFCFS();
            else
                fila = new FilaLCFS();
        }
    }
}
