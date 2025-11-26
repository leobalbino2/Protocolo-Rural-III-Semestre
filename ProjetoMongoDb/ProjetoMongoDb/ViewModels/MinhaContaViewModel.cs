namespace ProtocoloRural.ViewModels
{
    public class MinhaContaViewModel
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
    }

    public class AlterarEmailViewModel
    {
        public string EmailAtual { get; set; } = string.Empty;
        public string NovoEmail { get; set; } = string.Empty;
        public string ConfirmarEmail { get; set; } = string.Empty;
    }

    public class AlterarSenhaViewModel
    {
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
        public string ConfirmarSenha { get; set; } = string.Empty;
    }
}