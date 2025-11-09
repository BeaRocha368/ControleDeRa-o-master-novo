using System.ComponentModel.DataAnnotations;
using ControleDeRacao.Models;
using System;


namespace ControleDeRacao.Models
{
    public class AgendaAlimentacao
    {
        public int Id { get; set; }
        [Required]
        public int PetCodigo { get; set; }
        public required Pet Pet { get; set; }
        
        public DateTime Horario { get; set; }
        [Required]
        public string Turno { get; set; } // "Manhã", "Tarde", "Noite"
        [Required]
        public double QuantidadeKg { get; set; } // Quantidade de ração em kg


    }
}
