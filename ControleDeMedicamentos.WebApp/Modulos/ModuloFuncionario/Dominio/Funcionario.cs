using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

public class Funcionario : EntidadeBase<Funcionario>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;

    public Funcionario()
    {
    }

    public Funcionario(string nome,
   string telefone,
   string cpf
    )
    {
        Nome = nome.Trim();
        Telefone = telefone;
        Cpf = cpf;
    }


    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo Nome é obrigatório.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo Nome deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone) || !Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve seguir o formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.");

        if (string.IsNullOrWhiteSpace(Cpf) || !Regex.IsMatch(Cpf.Replace(".", "").Replace("-", ""), @"^\d{11}$"))
            erros.Add("O campo \"CPF\" deve conter exatamente 11 dígitos.");


        return erros;
    }

    public override void Atualizar(Funcionario funcionarioAtualizado)
    {
        Nome = funcionarioAtualizado.Nome;
        Telefone = funcionarioAtualizado.Telefone;
        Cpf = funcionarioAtualizado.Cpf;
    }

}