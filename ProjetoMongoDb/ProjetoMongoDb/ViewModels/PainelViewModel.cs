using System.Collections.Generic;
using ProtocoloRural.Models;

namespace ProtocoloRural.ViewModels
{
    public class PainelViewModel
    {
        public List<Avaliacao> Avaliacoes { get; set; } = new();
        public string? Mensagem { get; set; }
    }
}