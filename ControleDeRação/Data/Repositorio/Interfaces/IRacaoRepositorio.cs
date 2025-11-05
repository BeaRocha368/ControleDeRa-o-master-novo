using ControleDeRação.Models;

namespace ControleDeRação.Data.Repositorio.Interfaces
{
    public interface IRacaoRepositorio
    {
        Task AdicionarOuAtualizar(Racao racao);
        Task<Racao> BuscarPorPetId(int petId);
    }
}
