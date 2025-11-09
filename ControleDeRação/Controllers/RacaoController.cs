using Microsoft.AspNetCore.Mvc;
using ControleDeRacao.Models;
using ControleDeRacao.Data.Repositorio.Interfaces;
using System.Threading.Tasks;
using System;

namespace ControleDeRacao.Controllers
{
    public class RacaoController : Controller
    {
        private readonly IRacaoRepositorio _racaoRepositorio;

        public RacaoController(IRacaoRepositorio racaoRepositorio)
        {
            _racaoRepositorio = racaoRepositorio;
        }

        public async Task<IActionResult> Controle()
        {
            var racao = await _racaoRepositorio.BuscarEstoqueGlobal();

            if (racao == null)
            {
                // Inicializa o registro de estoque se ele não existir
                racao = new Racao
                {
                    Id = 1,
                    EstoqueAtualKg = 0.0m,
                    ConsumoDiarioKg = 0.5m, // Valor padrão inicial (500g)
                    DataAtualizacao = DateTime.Now
                };
                await _racaoRepositorio.AdicionarOuAtualizar(racao);
            }
            else
            {
                // 1. Calcula o tempo decorrido desde a última atualização
                TimeSpan tempoDecorrido = DateTime.Now - racao.DataAtualizacao;

                // Arredonda para baixo para obter o número inteiro de dias COMPLETOs que se passaram
                int diasPassados = (int)Math.Floor(tempoDecorrido.TotalDays);

                if (diasPassados >= 1)
                {
                    // 2. Calcula o consumo total e subtrai do estoque
                    decimal consumoTotal = diasPassados * racao.ConsumoDiarioKg;

                    racao.EstoqueAtualKg -= consumoTotal;

                    // Garante que o estoque não fique negativo
                    if (racao.EstoqueAtualKg < 0)
                    {
                        racao.EstoqueAtualKg = 0;
                    }

                    // 3. Atualiza a data de referência para o consumo
                    // Adicionamos os dias deduzidos à DataAtualizacao para manter o tempo restante de hoje
                    racao.DataAtualizacao = racao.DataAtualizacao.AddDays(diasPassados);

                    // 4. Salva a alteração no estoque
                    await _racaoRepositorio.AdicionarOuAtualizar(racao);
                }
            }

            return View(racao);
        }

        // Esta ação APENAS adiciona a compra e atualiza a configuração de consumo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizaEstoque(
            decimal quantidadeComprada,
            decimal consumoDiario,
            decimal estoqueAtual,
            DateTime dataCompra)
        {
            var racao = await _racaoRepositorio.BuscarEstoqueGlobal();

            if (racao == null)
            {
                racao = new Racao { Id = 1 };
            }

            racao.EstoqueAtualKg = estoqueAtual + quantidadeComprada;

            racao.ConsumoDiarioKg = consumoDiario;

            if (quantidadeComprada > 0)
            {
                racao.UltimaCompraKg = quantidadeComprada;
                racao.DataAtualizacao = dataCompra;
            }
            else
            {
                racao.DataAtualizacao = DateTime.Now;
            }

            await _racaoRepositorio.AdicionarOuAtualizar(racao);

            return RedirectToAction("Controle");
        }
    }

}
