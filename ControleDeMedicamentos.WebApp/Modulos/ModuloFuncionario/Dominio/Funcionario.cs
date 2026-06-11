using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

public class Funcionario : EntidadeBase<Funcionario>
{
    public string Nome { get; set; } = string.Empty;

    public Funcionario()
    {
    }

    public Funcionario(string nome)
    {
        Nome = nome.Trim();
    }

    public override void Atualizar(Funcionario funcionarioAtualizado)
    {
        Nome = funcionarioAtualizado.Nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo Nome é obrigatório.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo Nome deve conter entre 3 e 100 caracteres.");

        return erros;
    }
}