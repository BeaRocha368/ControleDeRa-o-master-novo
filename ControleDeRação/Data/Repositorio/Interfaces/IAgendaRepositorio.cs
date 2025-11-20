using ControleDeRacao.Models;

namespace ControleDeRacao.Data.Repositorio.Interfaces
{
    public interface IAgendaRepositorio
    {
        Task SalvarAgendaAsync(Agenda agenda);
        Task<Agenda> ObterAgendaAsync();

        Task<List<Agenda>> BuscarTodasAgendasAsync();

    }
}
