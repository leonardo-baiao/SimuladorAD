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
        private double tempoUltimoEvento = 0;
        private double tempoInicialRodada = 0;
        private int amostras = 0;
        private bool servidor = false;
        private Evento eventoAtual;
        private double TAXA_CHEGADA;
        private ListaEventos listaEventos;
        private Fila fila;
        private Estatistica estatisticaAtual;
        private List<Estatistica> listaEstatisticas;
        private readonly GeradorEstatisticas _geradorEstatisticas;

        public Simulador(TipoFila tipoFila, double taxaChegada)
        {
            GeraFila(tipoFila);
            TAXA_CHEGADA = taxaChegada;
            listaEventos = new ListaEventos();
            estatisticaAtual = new Estatistica { Rodada = 0 };
            listaEstatisticas = new List<Estatistica>();
            _geradorEstatisticas = new GeradorEstatisticas();
        }

        public int Rodada { get; private set; }
        
        public void ProximaRodada()
        {
            if(Rodada != 0)
                GeraRodadaEstatisticas();
            estatisticaAtual = new Estatistica { Rodada = ++Rodada };
            amostras = 0;
            tempoInicialRodada = tempo;
        }
        
        public void ProcessaEventos()
        {
            while(amostras < Constantes.KMIN)
            {
                TrataEvento();
            }
        }

        private void TrataEvento()
        {
            eventoAtual = listaEventos.ProximoEvento();

            if (eventoAtual == null)
            {
                listaEventos.AdicionaEvento(CalculaProximaChegada());
                return;
            }

            tempo = eventoAtual.Tempo;

            switch (eventoAtual.Tipo)
            {
                case TipoEvento.CHEGADA:
                    ChegadaFregues();
                    break;
                case TipoEvento.SAIDA:
                    AtendimentoFregues();
                    break;
                default:
                    break;
            }

            tempoUltimoEvento = tempo;
        }
        
        private void ChegadaFregues()
        {
            estatisticaAtual.QuantidadeMedia = estatisticaAtual.QuantidadeMedia + (fila.Quantidade * (tempo - tempoUltimoEvento)); 
            
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
            estatisticaAtual.QuantidadeMedia = estatisticaAtual.QuantidadeMedia + (fila.Quantidade * (tempo - tempoUltimoEvento));

            var cliente = fila.RetornaFregues();

            if (Rodada.Equals(cliente.Tipo))
            {
                _geradorEstatisticas.CalculaSomaAmostras(ref estatisticaAtual, tempo - cliente.TempoChegada);
                amostras++;
            }
            
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
                Tempo = tempo + _geradorEstatisticas.CalculaExponencial(TAXA_CHEGADA)
            };
        }
        
        private void GeraRodadaEstatisticas()
        {
            estatisticaAtual.TempoMedio = _geradorEstatisticas.CalculaMediaAmostral(estatisticaAtual.SomaAmostras, amostras);
            estatisticaAtual.QuantidadeMedia = _geradorEstatisticas.CalculaMediaAmostral(estatisticaAtual.QuantidadeMedia,tempo - tempoInicialRodada);
            listaEstatisticas.Add(estatisticaAtual);

            Console.WriteLine("");
            Console.WriteLine("Rodada " + Rodada);
            Console.WriteLine("Quantidade: " + fila.Quantidade);
            Console.WriteLine("Tempo Medio: " + estatisticaAtual.TempoMedio);
            Console.WriteLine("Quantidade Media: " + estatisticaAtual.QuantidadeMedia);
        }

        public void GeraEstatisticas()
        {
            double tempoMedioFinal;
            double varianciaTempoFinal;
            double somaTempoMedio = 0;
            double somaTempoQMedio = 0;

            double mediaPessoasFinal;
            double varianciaPessoasFinal;
            double somaQuantidadeMedia = 0;
            double somaQuantidadeQMedia = 0;

            IntervaloConfianca icMedia;
            IntervaloConfianca icVariancia;
            IntervaloConfianca icPessoasMedia;
            IntervaloConfianca icPessoasVariancia;
            
            foreach (var estatistica in listaEstatisticas)
            {
                somaTempoMedio += estatistica.TempoMedio;
                somaTempoQMedio += Math.Pow(estatistica.TempoMedio, 2);
                somaQuantidadeMedia += estatistica.QuantidadeMedia;
                somaQuantidadeQMedia += Math.Pow(estatistica.QuantidadeMedia, 2);
            }

            tempoMedioFinal = _geradorEstatisticas.CalculaMediaAmostral(somaTempoMedio, listaEstatisticas.Count);
            varianciaTempoFinal = _geradorEstatisticas.CalculaVarianciaAmostral(somaTempoMedio, somaTempoQMedio, listaEstatisticas.Count);

            mediaPessoasFinal = _geradorEstatisticas.CalculaMediaAmostral(somaQuantidadeMedia,listaEstatisticas.Count);
            varianciaPessoasFinal = _geradorEstatisticas.CalculaVarianciaAmostral(somaQuantidadeMedia,somaQuantidadeQMedia,listaEstatisticas.Count);

            icMedia = _geradorEstatisticas.CalculaIC(tempoMedioFinal, varianciaTempoFinal, VariavelAleatoria.TSTUDENT, listaEstatisticas.Count);
            icVariancia = _geradorEstatisticas.CalculaIC(tempoMedioFinal, varianciaTempoFinal, VariavelAleatoria.CHIQUADRADO, listaEstatisticas.Count);

            icPessoasMedia = _geradorEstatisticas.CalculaIC(mediaPessoasFinal, varianciaPessoasFinal, VariavelAleatoria.TSTUDENT, listaEstatisticas.Count);
            icPessoasVariancia = _geradorEstatisticas.CalculaIC(mediaPessoasFinal, varianciaPessoasFinal, VariavelAleatoria.CHIQUADRADO, listaEstatisticas.Count);

            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Rodadas: " + (listaEstatisticas.Count));
            Console.WriteLine("");
            Console.WriteLine("Tempo Medio: " + tempoMedioFinal);
            Console.WriteLine("Variancia Tempo: " + varianciaTempoFinal);
            Console.WriteLine("Intervalo de Confiança Media:");
            Console.WriteLine("    L: {0}, U: {1}, P: {2}", icMedia.L, icMedia.U, icMedia.Precisao);
            Console.WriteLine("Intervalo de Confiança Variancia:");
            Console.WriteLine("    L: {0}, U: {1}, P: {2}", icVariancia.L, icVariancia.U, icVariancia.Precisao);
            Console.WriteLine("");
            Console.WriteLine("Numero de Pessoas Medio: " + mediaPessoasFinal);
            Console.WriteLine("Variancia do numero de pessoas: " + varianciaPessoasFinal);
            Console.WriteLine("Intervalo de Confiança numero de pessoas medio:");
            Console.WriteLine("    L: {0}, U: {1}, P: {2}", icPessoasMedia.L, icPessoasMedia.U, icPessoasMedia.Precisao);
            Console.WriteLine("Intervalo de Confiança variancia numero de pessoas:");
            Console.WriteLine("    L: {0}, U: {1}, P: {2}", icPessoasVariancia.L, icPessoasVariancia.U, icPessoasVariancia.Precisao);
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
