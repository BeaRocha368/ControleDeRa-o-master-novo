using ControleDeRacao.Models;
using ControleDeRacao.Data.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ControleDeRacao.Data;

namespace ControleDeRacao.Data.Repositorio
{
    public class RacaoRepositorio : IRacaoRepositorio

    {

        private readonly BancoContexto _context;

        public RacaoRepositorio(BancoContexto context)

        {

            _context = context;

        }

        public async Task<Racao> BuscarEstoqueGlobal()

        {

            return await _context.Racoes.FirstOrDefaultAsync();

        }

        public async Task AdicionarOuAtualizar(Racao racao)

        {

            var existente = await _context.Racoes.FirstOrDefaultAsync();

            if (existente != null)

            {

                // Atualiza os campos, mantendo o Id existente

                existente.ConsumoDiarioKg = racao.ConsumoDiarioKg;

                existente.EstoqueAtualKg = racao.EstoqueAtualKg;

                existente.UltimaCompraKg = racao.UltimaCompraKg;

                existente.DataAtualizacao = racao.DataAtualizacao;

                _context.Racoes.Update(existente);

            }

            else

            {

                // Não existe registro → cria o primeiro

                _context.Racoes.Add(racao);

            }

            await _context.SaveChangesAsync();

        }

    }



}



