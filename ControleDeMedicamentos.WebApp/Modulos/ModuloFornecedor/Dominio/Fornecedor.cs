using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Cnpj { get; set; } = string.Empty;

    public Fornecedor()
    {
    }

    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome.Trim();
        Telefone = telefone.Trim();
        Cnpj = ObterSomenteDigitos(cnpj);
    }

    public override void Atualizar(Fornecedor fornecedorAtualizado)
    {
        Nome = fornecedorAtualizado.Nome;
        Telefone = fornecedorAtualizado.Telefone;
        Cnpj = fornecedorAtualizado.Cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo Nome é obrigatório.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo Nome deve conter entre 3 e 100 caracteres.");

        string telefoneSomenteDigitos = ObterSomenteDigitos(Telefone);

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo Telefone é obrigatório.");
        else if (telefoneSomenteDigitos.Length < 10 || telefoneSomenteDigitos.Length > 11)
            erros.Add("O campo Telefone deve possuir um formato válido.");

        string cnpjSomenteDigitos = ObterSomenteDigitos(Cnpj);

        if (string.IsNullOrWhiteSpace(Cnpj))
            erros.Add("O campo CNPJ é obrigatório.");
        else if (cnpjSomenteDigitos.Length != 14)
            erros.Add("O campo CNPJ deve conter 14 dígitos.");

        return erros;
    }

    public static string ObterSomenteDigitos(string valor)
    {
        return new string(valor.Where(char.IsDigit).ToArray());
    }
}