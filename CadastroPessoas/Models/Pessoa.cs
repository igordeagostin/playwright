using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CadastroPessoas.Models;

public class Pessoa
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um email válido.")]
    public string Email { get; set; }
    public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
}
