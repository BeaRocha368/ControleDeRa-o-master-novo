using Microsoft.AspNetCore.Mvc;
using ControleDeRacao.Models;
using ControleDeRacao.Data.Repositorio;
using System.Threading.Tasks;
using System;
using ControleDeRacao.Data.Repositorio.Interfaces;
using static System.Math;
using ControleDeRacao.Data.Repositorio.Interfaces.IPetRepositorio;

namespace ControleDeRacao.Controllers
{
    public class RacaoController : Controller
    {
        private readonly IRacaoRepositorio _racaoRepositorio;
        private readonly IPetRepositorio _petRepositorio;

        public RacaoController(IRacaoRepositorio racaoRepositorio, IPetRepositorio petRepositorio)
        {
            _racaoRepositorio = racaoRepositorio;
            _petRepositorio = petRepositorio;
        }

        public async Task<IActionResult> Controle()
        {
            var racao = await _racaoRepositorio.BuscarEstoqueGlobal();

            if (racao == null)
            {
                racao = new Racao
                {
                    Id = 1,
                    EstoqueAtualKg = 0.0m,
                    ConsumoDiarioKg = 0.5m,
                    DataAtualizacao = DateTime.Now
                };
                await _racaoRepositorio.AdicionarOuAtualizar(racao);
            }
            else
            {
                TimeSpan tempoDecorrido = DateTime.Now - racao.DataAtualizacao;
                int diasPassados = (int)Floor(tempoDecorrido.TotalDays);

                if (diasPassados >= 1)
                {
                    decimal consumoTotal = diasPassados * racao.ConsumoDiarioKg;
                    racao.EstoqueAtualKg -= consumoTotal;

                    if (racao.EstoqueAtualKg < 0)
                    {
                        racao.EstoqueAtualKg = 0;
                    }

                    // Atualiza a data de referência (após o consumo)
                    racao.DataAtualizacao = racao.DataAtualizacao.AddDays(diasPassados);
                    await _racaoRepositorio.AdicionarOuAtualizar(racao);
                }
            }

            var viewModel = new RacaoViewModel
            {
                EstoqueAtualKg = (double)racao.EstoqueAtualKg,
                ConsumoDiarioKg = (double)racao.ConsumoDiarioKg,
                DataAtualizacao = racao.DataAtualizacao
            };

            if (racao.Id > 0)
            {
                viewModel.Id = racao.Id;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BuscarPet(RacaoViewModel model)
        {
            await RecarregarDadosRacaoGlobal(model);

            if (!ModelState.IsValid)
            {
                return View("Controle", model);
            }

            var pet = await _petRepositorio.BuscarPorCodigo(model.CodigoAcesso);

            if (pet == null)
            {
                // PET não encontrado
                ModelState.AddModelError("CodigoAcesso", "Código do PET não encontrado.");
                model.NomePet = string.Empty;
            }
            else
            {
                // PET encontrado: preenche o nome
                model.NomePet = pet.Nome;
                ModelState.Remove("CodigoAcesso");
            }

            // Retorna a View com o nome preenchido (ou erro), mantendo os dados de estoque
            return View("Controle", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizaEstoque(RacaoViewModel model)
        {
            var racao = await _racaoRepositorio.BuscarEstoqueGlobal();

            if (racao == null)
            {
                racao = new Racao { Id = 1 };
            }

            racao.EstoqueAtualKg += (decimal)model.QuantidadeCompradaKg;

            racao.ConsumoDiarioKg = (decimal)model.ConsumoDiarioKg;

            if (model.QuantidadeCompradaKg > 0)
            {
                racao.DataAtualizacao = model.DataDaCompra ?? DateTime.Now;
            }
            else
            {
                racao.DataAtualizacao = DateTime.Now;
            }

            await _racaoRepositorio.AdicionarOuAtualizar(racao);

            if (!string.IsNullOrEmpty(model.NomePet))
            {
                TempData["MensagemSucesso"] = $"Estoque atualizado com sucesso! Interação registrada com PET: {model.NomePet}.";
            }
            else
            {
                TempData["MensagemSucesso"] = "Estoque atualizado com sucesso!";
            }

            return RedirectToAction("Controle");
        }

        private async Task RecarregarDadosRacaoGlobal(RacaoViewModel model)
        {
            var racao = await _racaoRepositorio.BuscarEstoqueGlobal();
            if (racao != null)
            {
                model.Id = racao.Id;
                model.EstoqueAtualKg = (double)racao.EstoqueAtualKg;
                model.ConsumoDiarioKg = (double)racao.ConsumoDiarioKg;
                model.DataAtualizacao = racao.DataAtualizacao;
            }
        }
    }
}
