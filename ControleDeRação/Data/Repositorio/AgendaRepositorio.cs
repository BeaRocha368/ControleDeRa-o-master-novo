using ControleDeRacao.Data.Repositorio.Interfaces;
using ControleDeRacao.Models;
using ControleDeRacao.Data.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ControleDeRacao.Data.Repositorio
{
    public class AgendaRepositorio : IAgendaRepositorio
    {
        private readonly BancoContexto _bancoContexto;

        public AgendaRepositorio(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
        }

        public async Task<Agenda> ObterAgendaAsync()
        {
            return await _bancoContexto.Agenda.FirstOrDefaultAsync();
        }

        public async Task SalvarAgendaAsync(Agenda agenda)
        {
            var agendaExistente = await _bancoContexto.Agenda.FirstOrDefaultAsync();

            if (agendaExistente != null)
            {
                agendaExistente.HorarioMatutino = agenda.HorarioMatutino;
                agendaExistente.HorarioVespertino = agenda.HorarioVespertino;
                agendaExistente.HorarioNoturno = agenda.HorarioNoturno;
                _bancoContexto.Agenda.Update(agendaExistente);
            }
            else
            {
                await _bancoContexto.Agenda.AddAsync(agenda);
            }

            await _bancoContexto.SaveChangesAsync();
        }

    }
}
