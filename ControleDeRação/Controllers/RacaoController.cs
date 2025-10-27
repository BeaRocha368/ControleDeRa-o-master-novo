using Microsoft.AspNetCore.Mvc;

namespace ControleDeRação.Controllers
{
    public class RacaoController : Controller
    {
            // Action: /Racao/Controle (Mapeia para asp-action="Controle")
            public IActionResult Controle()
            {
                // 1. CHAMA O MODEL: Busca o estoque atual, consumo diário e alerta de estoque.
                // 2. Retorna a view de controle de ração (View/Racao/Controle.cshtml)
                return View();
            }

            // Action POST: Atualiza o estoque ou consumo
            [HttpPost]
            public IActionResult AtualizarEstoque(decimal quantidadeComprada, decimal consumoDiario)
            {
                // Lógica para atualizar o Model (estoque)
                // Lógica para verificar se o estoque está baixo e acionar o alerta
                return RedirectToAction("Controle");
            }
    }
  
}
