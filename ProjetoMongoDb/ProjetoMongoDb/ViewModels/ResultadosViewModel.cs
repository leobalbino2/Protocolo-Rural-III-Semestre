using ProtocoloRural.Models;
using System.Collections.Generic;

namespace ProtocoloRural.ViewModels
{
    public class ResultadosViewModel
    {
        public Avaliacao Avaliacao { get; set; } = new Avaliacao();
        public List<Indicador> Indicadores { get; set; } = new List<Indicador>();
        public List<RespostaDisplayVm> Respostas { get; set; } = new List<RespostaDisplayVm>();
    }

    public class RespostaDisplayVm
    {
        public string IndicadorId { get; set; } = string.Empty;
        public string IndicadorNome { get; set; } = string.Empty;
        public int ParametroId { get; set; }
        public string ParametroTexto { get; set; } = string.Empty;
        public int Valor { get; set; }
        public string? Texto { get; set; }
    }
}