using System;
using System.Collections.Generic;
using System.Text;

namespace Estruturas
{
    public class Evento
    {
        public TipoEvento Tipo { get; set; }
        public double Tempo { get; set; }
    }

    public enum TipoEvento
    {
        CHEGADA = 1,
        SAIDA = 2,
    }
}
