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

        public async Task SalvarAgendaAsync(Agenda agenda)
        {
            await _bancoContexto.Agenda.AddAsync(agenda);
            await _bancoContexto.SaveChangesAsync();
        }
     
    }
}
