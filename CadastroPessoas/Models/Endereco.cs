namespace CadastroPessoas.Models;

public class Endereco
{
    public int Id { get; set; }
    public string Rua { get; set; }
    public string Cidade { get; set; }
    public string Uf { get; set; }

    public int IdPessoa { get; set; }
    public Pessoa Pessoa { get; set; }
}
