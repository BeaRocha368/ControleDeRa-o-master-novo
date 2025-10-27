using Microsoft.AspNetCore.Mvc;

namespace ControleDeRação.Controllers
{
    public class RelatoriosController : Controller
    {
       
            // Action: /Relatorios/Listar (Mapeia para asp-action="Listar")
            public IActionResult Listar()
            {
                // 1. CHAMA O MODEL: Busca dados históricos de consumo (mês, quantidade, marca)
                // 2. Lógica para calcular a previsão de término do estoque.
                // Retorna a view dos relatórios (View/Relatorios/Listar.cshtml)
                return View();
            }
        
    }

}
