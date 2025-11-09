using ControleDeRacao.Models;
using ControleDeRacao.Data.Repositorio.Interfaces.IPetRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ControleDeRacao.Data;

namespace ControleDeRacao.Data.Repositorio
{
    public class PetRepositorio : IPetRepositorio
    {
        private readonly BancoContexto _context;

        // Injeção de Dependência do BancoContexto
        public PetRepositorio(BancoContexto context)
        {
            _context = context;
        }

        // Implementação do Adicionar
        public async Task Adicionar(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        // Implementação do BuscarPorCodigo
        public async Task<Pet> BuscarPorCodigo(string codigo)
        {
            return await _context.Pets
                                 .FirstOrDefaultAsync(p => p.CodigoAcesso == codigo);
        }

        // Implementação do BuscarTodos
        public async Task<List<Pet>> BuscarTodos()
        {
            return await _context.Pets.ToListAsync();
        }
    }

}

