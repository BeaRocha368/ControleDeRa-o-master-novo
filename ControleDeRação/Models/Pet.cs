namespace ControleDeRação.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public double Peso { get; set; }
        public string MarcaRacao { get; set; }
        public Guid CodigoAcesso { get; set; } = Guid.NewGuid(); // Código de Acesso do Pet - Gerado e único
        public DateTime DataCriacao { get; set; } = DateTime.Now; // Propriedade para a data de criação do registro
    }
}
