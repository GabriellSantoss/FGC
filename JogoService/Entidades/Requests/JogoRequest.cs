namespace JogoService.Requests
{
    public class JogoRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Tamanho { get; set; }
        public int Preco { get; set; }
        public long IdCategoria { get; set; }
        public int IdadeMinima { get; set; }
        public bool Ativo { get; set; }
        public long IdFornecedor { get; set; }
    }
}