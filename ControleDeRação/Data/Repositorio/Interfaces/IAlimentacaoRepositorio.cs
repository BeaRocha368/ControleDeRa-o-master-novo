using ControleDeRacao.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeRacao.Data.Repositorio.Interfaces

{

    public interface IAlimentacaoRepositorio

    {

        Task<List<AgendaAlimentacao>> Listar(int petCodigo);

        Task<AgendaAlimentacao> BuscarPorHorario(int petCodigo, DateTime horario);

        Task<AgendaAlimentacao> Criar(AgendaAlimentacao agenda);

    }

}

