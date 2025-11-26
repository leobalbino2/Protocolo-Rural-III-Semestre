using ProtocoloRural.Models;
using System.Collections.Generic;

namespace ProtocoloRural.ViewModels
{
    public class AvaliacaoViewModel
    {
        public Avaliacao Avaliacao { get; set; } = new Avaliacao();
        public List<IndicadorVm> Indicadores { get; set; } = new List<IndicadorVm>();
    }

    public class IndicadorVm
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<ParametroVm> Parametros { get; set; } = new List<ParametroVm>();
    }

    public class ParametroVm
    {
        public int Index { get; set; }
        public string Texto { get; set; } = string.Empty;
        public int Valor { get; set; }
    }
}