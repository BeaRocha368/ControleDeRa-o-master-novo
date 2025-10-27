using Microsoft.AspNetCore.Mvc;

namespace ControleDeRação.Controllers
{
    public class AlimentacaoController : Controller
    { 
            // Action: /Alimentacao/Agenda (Mapeia para asp-action="Agenda")
            public IActionResult Agenda()
            {
                // Regra de Negócio: Verifica se há um Pet cadastrado antes de exibir a agenda
                // 1. Se não houver pet, redireciona para o cadastro.

                // 2. Se houver pet, CHAMA O MODEL para buscar a agenda do pet.
                // Retorna a view da agenda (View/Alimentacao/Agenda.cshtml)
                return View();
            }

            // Action POST: Salva os horários e turnos
            [HttpPost]
            public IActionResult SalvarAgenda(string matutino, string vespertino, string noturno)
            {
                // Lógica para salvar a agenda (Horários 07:00, 13:00, 19:00, etc.)
                return RedirectToAction("Agenda");
            }
    }
}
