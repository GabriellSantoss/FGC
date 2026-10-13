namespace JogoService.Responses
{
    public class JogoResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Tamanho { get; set; }
        public int Preco { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int IdadeMinima { get; set; }
        public bool Ativo { get; set; }
        public int NumeroVendas { get; set; }
        public string EmpresaFornecedora { get; set; } = string.Empty;
    }
}