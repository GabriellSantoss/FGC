namespace UsuarioService.Requests
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataNascimento { get; set; }

        public long PerfilId { get; set; }
    }
}