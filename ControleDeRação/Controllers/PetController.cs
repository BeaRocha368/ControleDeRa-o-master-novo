using Microsoft.AspNetCore.Mvc;
using ControleDeRação.Models;
using ControleDeRação.Data;
using Microsoft.EntityFrameworkCore;
using ControleDeRação.Data.Repositorio.Interfaces.IPetRepositorio;

namespace ControleDeRação.Controllers
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
                await _petRepositorio.Adicionar(novoPet);

                return RedirectToAction("ResultadoConsulta", new { codigo = novoPet.CodigoAcesso });
            }
            return View(novoPet);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarCodigo(string codigo)
        {
            if (Guid.TryParse(codigo, out Guid codigoGuid))
            {
                var petEncontrado = await _petRepositorio.BuscarPorCodigo(codigoGuid);

                if (petEncontrado != null)
                {
                    return View("ResultadoConsulta", petEncontrado);
                }
            }
            return RedirectToAction("Codigo");
        }

        public async Task<IActionResult> ResultadoConsulta(Guid codigo)
        {
            var petEncontrado = await _petRepositorio.BuscarPorCodigo(codigo);
            return View(petEncontrado);
        }
    }
}

