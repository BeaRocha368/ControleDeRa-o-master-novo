using ControleDeRacao.Models;

namespace ControleDeRacao.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public double Peso { get; set; }
        public string? MarcaRacao { get; set; }

        [Microsoft.AspNetCore.Mvc.ModelBinding.BindNever]
        public string? CodigoAcesso { get; set; }

        [Microsoft.AspNetCore.Mvc.ModelBinding.BindNever]
        public DateTime DataCriacao { get; set; } = DateTime.Now; // Propriedade para a data de criação do registro
    }
}
