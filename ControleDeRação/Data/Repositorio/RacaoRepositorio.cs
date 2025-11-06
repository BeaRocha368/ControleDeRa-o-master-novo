using ControleDeRação.Models;
using ControleDeRação.Data.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ControleDeRação.Data.Repositorio
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
            if (racao.Id > 0 && _context.Racoes.Any(r => r.Id == racao.Id)) // Verifica se já existe
            {
                _context.Racoes.Update(racao);
            }
            else
            {
                racao.Id = 1; // Força o ID para garantir que só haja um registro
                _context.Racoes.Add(racao);
            }
            await _context.SaveChangesAsync();
        }
    }
}

