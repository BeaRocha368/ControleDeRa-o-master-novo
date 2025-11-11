using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControleDeRacao.Models
{
    public class RacaoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Estoque Atual (KG)")]
        [BindNever] // Não será enviado de volta no formulário
        public double EstoqueAtualKg { get; set; } 

        [Display(Name = "Consumo Diário (KG/dia)")]
        [Required(ErrorMessage = "O consumo diário é obrigatório.")]
        [Range(0.01, 10.0, ErrorMessage = "O consumo deve ser um valor válido em KG.")]
        public double ConsumoDiarioKg { get; set; }

        [Display(Name = "Data da Última Atualização")]
        [BindNever]
        public DateTime DataAtualizacao { get; set; }

        // --- CAMPOS NECESSÁRIOS PARA A NOVA FUNCIONALIDADE ---

        // Campo para o usuário digitar o código (Obrigatório para a busca)
        [Required(ErrorMessage = "O Código do PET é obrigatório para a busca.")]
        [Display(Name = "Código do PET")]
        public string CodigoAcesso { get; set; } = string.Empty;

        // Campo para exibir o nome do PET (Preenchido após a busca, não é um campo de input)
        [Display(Name = "Nome do PET")]
        [BindNever]
        public string NomePet { get; set; } = string.Empty;

        // Campo para a compra (mantido do seu formulário)
        [Display(Name = "Quantidade Comprada (KG)")]
        [Range(0.0, 1000.0, ErrorMessage = "A quantidade deve ser positiva.")]
        public double QuantidadeCompradaKg { get; set; }

        // Data da compra
        [DataType(DataType.Date)]
        [Display(Name = "Data da Compra")]
        public DateTime? DataDaCompra { get; set; }
    }
}
