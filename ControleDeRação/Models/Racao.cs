using System;
using System.ComponentModel.DataAnnotations.Schema;
using ControleDeRação.Models;

namespace ControleDeRação.Models
{
    public class Racao
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        // Propriedade de Navegação (para carregar o Pet junto com a Racao)
        [ForeignKey("PetId")]
        public Pet Pet { get; set; }
        public decimal ConsumoDiarioKg { get; set; }
        public decimal EstoqueAtualKg { get; set; }
        public decimal UltimaCompraKg { get; set; }
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

        [NotMapped] // Não cria coluna no banco, é calculado em tempo de execução
        public int DiasRestantes
        {
            get
            {
                if (ConsumoDiarioKg > 0)
                {
                    return (int)Math.Floor(EstoqueAtualKg / ConsumoDiarioKg);
                }
                return 0;
            }
        }

        // Propriedade Calculada: Verifica se o estoque está baixo (ex: menos de 7 dias)
        [NotMapped]
        public bool AlertaEstoqueBaixo => DiasRestantes < 7;
    }
}
