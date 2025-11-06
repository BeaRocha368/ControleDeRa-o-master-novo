namespace ControleDeRação.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public double Peso { get; set; }
        public string MarcaRacao { get; set; }
        public string CodigoAcesso { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now; // Propriedade para a data de criação do registro
    }
}
