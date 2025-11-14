using Microsoft.AspNetCore.Mvc;
using ControleDeRacao.Models;
using ControleDeRacao.Data;
using Microsoft.EntityFrameworkCore;
using ControleDeRacao.Data.Repositorio.Interfaces.IPetRepositorio;
using ControleDeRacao.Helpers;

namespace ControleDeRacao.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetRepositorio _petRepositorio;

        public PetController(IPetRepositorio petRepositorio)
        {
            _petRepositorio = petRepositorio;
        }

        // Action: /Pet/Cadastrar (Mapeia para asp-action="Cadastrar")
        public IActionResult Cadastrar()
        {
            // Retorna o formulário de cadastro (View/Pet/cadastrar.cshtml)
            return View();
        }

        [HttpPost]
        [ValidateAntiforgeryToken]
        public async Task<IActionResult> Cadastrar(Pet novoPet)
        {
            if (ModelState.IsValid)
            {
               
                novoPet.DataCriacao = DateTime.Now;
                novoPet.CodigoAcesso = CodigoPetHelper.GerarCodigoCurto(6);

                await _petRepositorio.Adicionar(novoPet);
                return RedirectToAction("CadastroConcluido", new { codigo = novoPet.CodigoAcesso });
            }

            return View(novoPet);
        }

        public async Task<IActionResult> CadastroConcluido(string codigo)
        {
            // Busca o pet recém-cadastrado para passar os dados para a View
            var petRecemCadastrado = await _petRepositorio.BuscarPorCodigo(codigo);

            // Retorna a View "CadastroConcluido.cshtml"
            return View("CadastroConcluido", petRecemCadastrado);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarCodigo(string codigo)
        {
            var petEncontrado = await _petRepositorio.BuscarPorCodigo(codigo);

            if (petEncontrado != null)
            {

                return RedirectToAction("ResultadoConsulta", new { codigo = codigo });
            }
                        
            TempData["MensagemErro"] = "Código PET não encontrado ou inválido.";
            return RedirectToAction("Codigo");
        }
        public async Task<IActionResult> ResultadoConsulta(string codigo)
        {
            var petEncontrado = await _petRepositorio.BuscarPorCodigo(codigo);
            return View("DetalhesPet", petEncontrado);
        }

        public IActionResult Codigo()
        {
            return View();
        }
    }
}

