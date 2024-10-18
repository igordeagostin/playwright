using CadastroPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoas.Dados;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoa { get; set; }
    public DbSet<Endereco> Endereco { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>()
            .HasMany(p => p.Enderecos)
            .WithOne(a => a.Pessoa)
            .HasForeignKey(a => a.IdPessoa);
    }
}
