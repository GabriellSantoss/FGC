using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UsuarioService.Entidades;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace UsuarioService.Model
{
    public class Usuario(string nome, string sobrenome, string apelido, string email, string hashSenha, DateTime dataNascimento, long perfilId, string criadoPor)
         : EntidadeBase(criadoPor)
    {
        public string Nome { get; set; } = nome;
        public string Sobrenome { get; set; } = sobrenome;
        public string Apelido { get; set; } = apelido;
        public string Email { get; set; } = email;
        [JsonIgnore]
        public string HashSenha { get; set; } = hashSenha;
        public DateTime DataNascimento { get; set; } = dataNascimento;
        public long PerfilId { get; set; } = perfilId;
        public Perfil Perfil { get; set; }

        [NotMapped]
        [JsonPropertyName("idade")]
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }
    

    }
}
