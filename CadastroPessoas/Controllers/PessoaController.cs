using CadastroPessoas.Dados;
using CadastroPessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoas.Controllers;

public class PessoaController : Controller
{
    private readonly Contexto _context;

    public PessoaController(Contexto context)
    {
        _context = context;
    }

    // Listagem
    public async Task<IActionResult> Index()
    {
        return View(await _context.Pessoa.Include(p => p.Enderecos).ToListAsync());
    }

    // Criar (GET)
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pessoa pessoa)
    {
        _context.Add(pessoa); // Adiciona a pessoa e seus endereços
        await _context.SaveChangesAsync(); // Salva no banco de dados
        return RedirectToAction(nameof(Index)); // Redireciona para a listagem
    }

    // Edit (GET)
    public async Task<IActionResult> Edit(int id)
    {
        var pessoa = await _context.Pessoa
            .Include(p => p.Enderecos)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pessoa == null)
        {
            return NotFound(); // Retorna 404 se a pessoa não for encontrada
        }

        return View(pessoa); // Retorna a view de edição
    }

    // Editar (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Pessoa pessoa)
    {
        if (id != pessoa.Id)
            return NotFound();

        _context.Update(pessoa);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Excluir
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var pessoa = await _context.Pessoa.FindAsync(id);
        if (pessoa == null)
            return NotFound();

        return View(pessoa);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var person = await _context.Pessoa.FindAsync(id);
        if (person != null)
        {
            _context.Pessoa.Remove(person); // Remove a pessoa do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
        }
        return RedirectToAction(nameof(Index)); // Redireciona para a lista
    }

    // Details (GET)
    public async Task<IActionResult> Details(int id)
    {
        var person = await _context.Pessoa.Include(p => p.Enderecos).FirstOrDefaultAsync(p => p.Id == id);
        if (person == null)
        {
            return NotFound(); // Retorna 404 se a pessoa não for encontrada
        }
        return View(person); // Retorna a view de detalhes
    }

}
