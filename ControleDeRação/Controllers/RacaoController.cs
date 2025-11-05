using Microsoft.AspNetCore.Mvc;
using ControleDeRação.Models;
using ControleDeRação.Data.Repositorio.Interfaces;
using System.Threading.Tasks;
using System;

namespace ControleDeRação.Controllers
{
    public class RacaoController : Controller
    {
        private readonly IRacaoRepositorio _racaoRepositorio;

        public RacaoController(IRacaoRepositorio racaoRepositorio)
        {
            _racaoRepositorio = racaoRepositorio;
        }

        // Exibe o formulário de controle de estoque para um Pet
        public async Task<IActionResult> Controle(int petId)
        {
            // 1. Busca o registro de Ração/Estoque para o PetId
            var racao = await _racaoRepositorio.BuscarPorPetId(petId);

            // 2. Se o registro não existir, cria um novo (com valores padrão)
            if (racao == null)
            {
                if (petId == 0) return NotFound();

                racao = new Racao
                {
                    PetId = petId,
                    ConsumoDiarioKg = 0, // Padrão
                    EstoqueAtualKg = 0,  // Padrão
                    UltimaCompraKg = 0
                };
            }

            // Passa o Model para a View
            return View(racao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> AtualizaEstoque(
            int petId, // ID do Pet (escondido no formulário da View)
            decimal quantidadeComprada, // O KG adicionado ao estoque
            decimal consumoDiario,      // O novo KG de consumo
            decimal estoqueAtual)       // O estoque atual mostrado na tela
        {
            // 1. Busca o registro de Ração existente ou cria um novo
            var racao = await _racaoRepositorio.BuscarPorPetId(petId);

            if (racao == null)
            {
                racao = new Racao { PetId = petId };
            }

            // 2. Lógica de Atualização do Estoque

            racao.EstoqueAtualKg += quantidadeComprada;

            // 3. Lógica de Atualização do Consumo Diário e Histórico

            racao.ConsumoDiarioKg = consumoDiario;

            if (quantidadeComprada > 0)
            {
                racao.UltimaCompraKg = quantidadeComprada;
            }

            racao.DataAtualizacao = DateTime.Now;

            // 4. Salva ou Atualiza no banco de dados via Repositório
            await _racaoRepositorio.AdicionarOuAtualizar(racao);

            // 5. Redireciona de volta para a tela de controle, passando o PetId
            // para que a tela possa recarregar com os novos dados calculados
            return RedirectToAction("Controle", new { petId = petId });
        }
    }
  
}
