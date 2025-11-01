using Microsoft.AspNetCore.Mvc;
using ControleDeRação.Models;
using ControleDeRação.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleDeRação.Controllers
{
    public class PetController : Controller
    {
        private readonly BancoContexto _context;

        public PetController(BancoContexto context)
        {
            _context = context;
        }

        // Action: /Pet/Cadastrar (Mapeia para asp-action="Cadastrar")
        public IActionResult Cadastrar()
        {
            // Retorna o formulário de cadastro (View/Pet/cadastrar.cshtml)
            return View();
        }

        // Action POST: Recebe dados do formulário de Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Pet novoPet)
        {
            if (ModelState.IsValid)
            {
                // 1. CHAMA O MODEL: Salva o novoPet no banco de dados
                // O CodigoAcesso foi gerado automaticamente no Pet.cs antes de chegar aqui.
                _context.Pets.Add(novoPet); // Adiciona o Pet ao contexto
                await _context.SaveChangesAsync(); // Salva no banco de forma assíncrona

                // 2. Regra de Negócio: Gerar o Código PET após o salvamento
                // O objeto novoPet agora tem o ID e o CodigoAcesso salvos no banco.

                // 3. Redireciona para a tela de Código PET para que o usuário veja
                // Passando o Código de Acesso do Pet
                return RedirectToAction("ResultadoConsulta", new { codigo = novoPet.CodigoAcesso });
            }
            // Se falhar, retorna a view com os dados para correção
            return View(novoPet);
        }

        // Action: /Pet/Codigo (Mapeia para asp-action="Codigo")
        public IActionResult Codigo()
        {
            // Lógica para buscar o código do último pet cadastrado ou de um pet específico
            return View();
        }
        // ... (Actions Cadastrar, etc.)

        // Action: /Pet/Codigo (Mapeia para asp-action="Codigo")
        // 1. Exibe o formulário de consulta de código.

        // Action POST: Recebe o código digitado e busca o pet
        [HttpPost]
        public async Task<IActionResult> ConsultarCodigo(string codigo)
        {
            // 1. Lógica de busca: 
            // Tenta converter a string de código (digitada pelo usuário) para Guid
            if (Guid.TryParse(codigo, out Guid codigoGuid))
            {
                // Busca o Pet no banco de forma assíncrona
                var petEncontrado = await _context.Pets
                                                .FirstOrDefaultAsync(p => p.CodigoAcesso == codigoGuid);

                // 2. Se pet encontrado, retorna a view com os dados do pet.
                if (petEncontrado != null)
                {
                    // A View ResultadoConsulta deve estar em Views/Pet/ResultadoConsulta.cshtml
                    return View("ResultadoConsulta", petEncontrado);
                }
            }

            // 3. Se não encontrado (ou código inválido), retorna à tela de consulta com mensagem de erro.
            TempData["MensagemErro"] = "Código PET não encontrado ou inválido.";
            return RedirectToAction("Codigo");
        }

        // Action GET: Para ser usada após o cadastro (Redirecionamento)
        // Recebe o Guid gerado do cadastro e exibe o resultado/código
        public async Task<IActionResult> ResultadoConsulta(Guid codigo)
        {
            // Busca o pet recém-cadastrado no banco para garantir que ele existe
            var petEncontrado = await _context.Pets
                                            .FirstOrDefaultAsync(p => p.CodigoAcesso == codigo);

            if (petEncontrado == null)
            {
                // Caso ocorra um erro estranho e o código não encontre nada
                TempData["MensagemErro"] = "Pet não encontrado.";
                return RedirectToAction("Codigo");
            }

            return View(petEncontrado);
        }

    }
}

