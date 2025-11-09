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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Pet novoPet)
        {
            if (ModelState.IsValid)
            {
                novoPet.CodigoAcesso = CodigoPetHelper.GerarCodigoCurto(6);

                await _petRepositorio.Adicionar(novoPet);

                return RedirectToAction("ResultadoConsulta", new { codigo = novoPet.CodigoAcesso });
            }
            return View(novoPet);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarCodigo(string codigo)
        {
            var petEncontrado = await _petRepositorio.BuscarPorCodigo(codigo);

            if (petEncontrado != null)
            {
                
                return RedirectToAction("Controle", "Racao");
            }
                        
            TempData["MensagemErro"] = "Código PET não encontrado ou inválido.";
            return RedirectToAction("Codigo");
        }
        public async Task<IActionResult> ResultadoConsulta(string codigo)
        {
            var petEncontrado = await _petRepositorio.BuscarPorCodigo(codigo);
            return View(petEncontrado);
        }

        public IActionResult Codigo()
        {
            return View();
        }
    }
}

