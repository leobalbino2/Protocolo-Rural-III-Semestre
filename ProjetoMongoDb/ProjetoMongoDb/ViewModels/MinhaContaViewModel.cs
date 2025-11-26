namespace ProtocoloRural.ViewModels
{
    public class MinhaContaViewModel
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Mensagem { get; set; }
    }

    public class AlterarEmailViewModel
    {
        public string EmailAtual { get; set; }
        public string NovoEmail { get; set; }
        public string ConfirmarEmail { get; set; }
    }

    public class AlterarSenhaViewModel
    {
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}