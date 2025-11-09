using System;
using System.ComponentModel.DataAnnotations.Schema;
using ControleDeRacao.Models;

namespace ControleDeRacao.Models
{
    public class Racao
    {
        public int Id { get; set; } 
       
        public decimal ConsumoDiarioKg { get; set; }

        public decimal EstoqueAtualKg { get; set; }

        public decimal UltimaCompraKg { get; set; }

        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

        [NotMapped]
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

        // Propriedade Calculada: Verifica se o estoque está baixo (menor que 0.5 kg)
        [NotMapped]
        public bool AlertaEstoqueBaixo => EstoqueAtualKg <= 0.5M; // 0.5M = 500 gramas
    }
}
