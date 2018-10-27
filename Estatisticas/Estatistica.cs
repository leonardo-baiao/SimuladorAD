using System;
using System.Collections.Generic;
using System.Text;

namespace Estatisticas
{
    public class Estatistica
    {
        public int Rodada { get; set; }
        public int QuantidadeAmostras { get; set; }
        public double SomaAmostras { get; set; }
        public double SomaQAmostras { get; set; }
        public double TempoMedio { get; set; }
        public double TempoQMedio { get; set; }
        public double QuantidadeMedia { get; set; }
    }
}
