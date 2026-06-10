
using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

public sealed class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string CartaoSus { get; set; } = string.Empty;

    public string Cpf { get; set; } = string.Empty;

    public Paciente() { }

    public Paciente(
   string nome,
   string telefone,
   string cartaoSus,
   string cpf
  )
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone) || !Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve seguir o formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.");

        if (string.IsNullOrWhiteSpace(CartaoSus) || !Regex.IsMatch(CartaoSus.Replace(" ", ""), @"^\d{15}$"))
            erros.Add("O campo \"Cartão do SUS\" deve conter exatamente 15 dígitos.");

        if (string.IsNullOrWhiteSpace(Cpf) || !Regex.IsMatch(Cpf.Replace(".", "").Replace("-", ""), @"^\d{11}$"))
            erros.Add("O campo \"CPF\" deve conter exatamente 11 dígitos.");

        return erros;
    }

    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CartaoSus = entidadeAtualizada.CartaoSus;
        Cpf = entidadeAtualizada.Cpf;
    }
}


