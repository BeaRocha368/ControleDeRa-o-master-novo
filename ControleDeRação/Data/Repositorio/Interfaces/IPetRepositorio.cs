using Microsoft.AspNetCore.Mvc;
using ControleDeRacao.Models;
using ControleDeRacao.Data;
using Microsoft.EntityFrameworkCore;
using ControleDeRacao.Data.Repositorio.Interfaces;

namespace ControleDeRacao.Data.Repositorio.Interfaces.IPetRepositorio
{
    public interface IPetRepositorio
    {
        Task Adicionar(Pet pet);
        Task<Pet> BuscarPorCodigo(string codigo);
        Task<List<Pet>> BuscarTodos();
    }
}
