using Microsoft.AspNetCore.Mvc;
using ControleDeRação.Models;
using ControleDeRação.Data;
using Microsoft.EntityFrameworkCore;
using ControleDeRação.Data.Repositorio.Interfaces;

namespace ControleDeRação.Data.Repositorio.Interfaces.IPetRepositorio
{
    public interface IPetRepositorio
    {
        Task Adicionar(Pet pet);
        Task<Pet> BuscarPorCodigo(Guid codigo);
        Task<List<Pet>> BuscarTodos();
    }
}
