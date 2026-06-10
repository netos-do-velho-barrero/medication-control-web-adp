using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;

public record CadastrarFornecedorViewModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Nome deve conter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", ErrorMessage = "O campo Telefone deve possuir um formato válido.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
    [RegularExpression(@"^\d{14}$", ErrorMessage = "O campo CNPJ deve conter exatamente 14 dígitos.")]
    public string Cnpj { get; set; } = string.Empty;
}

public record EditarFornecedorViewModel : CadastrarFornecedorViewModel
{
    public string Id { get; set; } = string.Empty;
}

public record ListarFornecedorViewModel
{
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Cnpj { get; set; } = string.Empty;
}

public record ExcluirFornecedorViewModel
{
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public string Cnpj { get; set; } = string.Empty;
}