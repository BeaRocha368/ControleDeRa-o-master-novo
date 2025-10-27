using Microsoft.AspNetCore.Mvc;
using ControleDeRação.Models;

namespace ControleDeRação.Controllers
{
    public class PetController : Controller
    {
        // Action: /Pet/Cadastrar (Mapeia para asp-action="Cadastrar")
        public IActionResult Cadastrar()
        {
            // Retorna o formulário de cadastro (View/Pet/cadastrar.cshtml)
            return View();
        }

        // Action POST: Recebe dados do formulário de Cadastro
        [HttpPost]
        public IActionResult Cadastrar(Pet novoPet)
        {
            if (ModelState.IsValid)
            {
                // 1. CHAMA O MODEL: Salva o novoPet no banco de dados
                // 2. Regra de Negócio: Gerar o Código PET após o salvamento
                // 3. Redireciona para a tela de Código PET ou lista de pets
                return RedirectToAction("Index", "Home");
            }
            // Se falhar, retorna a view com os dados para correção
            return View(novoPet);
        }

        // Action: /Pet/Codigo (Mapeia para asp-action="Codigo")
        public IActionResult Codigo()
        {
            // Lógica para buscar o código do último pet cadastrado ou de um pet específico
            return View();
        }
    }
}