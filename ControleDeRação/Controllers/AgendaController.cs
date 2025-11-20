using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ControleDeRacao.Models;
using ControleDeRacao.Data.Repositorio.Interfaces;
using ControleDeRacao.Data.Repositorio.Interfaces.IPetRepositorio;

namespace ControleDeRacao.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAgendaRepositorio _agendaRepositorio;

        public AgendaController(IAgendaRepositorio agendaRepositorio)
        {
            _agendaRepositorio = agendaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Agenda()
        {
            var agendas = await _agendaRepositorio.BuscarTodasAgendasAsync();

            return View(agendas);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarAgenda(string? HorarioMatutino, string? HorarioVespertino, string? HorarioNoturno)
        {
            var agenda = new Agenda()
            {
                HorarioMatutino = HorarioMatutino,
                HorarioVespertino = HorarioVespertino,
                HorarioNoturno = HorarioNoturno
            };


            await _agendaRepositorio.SalvarAgendaAsync(agenda); 
            TempData["MensagemSucesso"] = "Agenda salva com sucesso!"; 

            return RedirectToAction("Agenda");
        }

        
    }
}
