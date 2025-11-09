using ControleDeRacao.Models;

namespace ControleDeRacao.Data.Repositorio.Interfaces
{
    public interface IRacaoRepositorio
    {
        Task AdicionarOuAtualizar(Racao racao);
        Task<Racao> BuscarEstoqueGlobal();
    }
}
