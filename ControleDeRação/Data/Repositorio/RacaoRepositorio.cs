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

        // Busca a entrada de racionamento de um pet específico
        public async Task<Racao> BuscarPorPetId(int petId)
        {
            return await _context.Racoes
                                 .Include(r => r.Pet)
                                 .FirstOrDefaultAsync(r => r.PetId == petId);
        }

        public async Task AdicionarOuAtualizar(Racao racao)
        {
            if (racao.Id > 0)
            {
                _context.Racoes.Update(racao);
            }
            else 
            {
                _context.Racoes.Add(racao);
            }
            await _context.SaveChangesAsync();
        }
    }
}

