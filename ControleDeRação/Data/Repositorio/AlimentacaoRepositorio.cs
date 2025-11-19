using ControleDeRacao.Models;

using ControleDeRacao.Data.Repositorio.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;

using System.Collections.Generic;

using System.Threading.Tasks;

namespace ControleDeRacao.Data.Repositorio

{

    public class AlimentacaoRepositorio : IAlimentacaoRepositorio

    {

        private readonly BancoContexto _context;

        public AlimentacaoRepositorio(BancoContexto context)

        {

            _context = context;

        }

        public async Task<List<AgendaAlimentacao>> Listar(int petCodigo)

        {

            return await _context.AgendaAlimentacoes

                .Include(x => x.Pet)

                .Where(x => x.PetCodigo == petCodigo)

                .OrderBy(x => x.Horario)

                .ToListAsync();

        }

        public async Task<AgendaAlimentacao> BuscarPorHorario(int petCodigo, DateTime horario)

        {

            return await _context.AgendaAlimentacoes

                .FirstOrDefaultAsync(x => x.PetCodigo == petCodigo && x.Horario == horario);

        }

        public async Task<AgendaAlimentacao> Criar(AgendaAlimentacao agenda)

        {

            // ✅ Regra: Não permitir repetir o mesmo horário

            var existe = await BuscarPorHorario(agenda.PetCodigo, agenda.Horario);

            if (existe != null)

                throw new Exception("Este horário já foi registrado para este pet.");

            // ✅ Salvar no banco

            _context.AgendaAlimentacoes.Add(agenda);

            await _context.SaveChangesAsync();


            // 💾 Salvar nova alimentação
            _context.AgendaAlimentacoes.Add(agenda);

            // 🔽 Descontar ração automaticamente
            var racao = await _context.Racoes.FirstOrDefaultAsync();
            if (racao != null)
            {


                if (racao.EstoqueAtualKg < 0)
                    racao.EstoqueAtualKg = 0; // Evita número negativo

                _context.Racoes.Update(racao);
            }

            // 💾 Salvar todas as alterações de uma vez
            await _context.SaveChangesAsync();

            return agenda;

        }

    }

}

