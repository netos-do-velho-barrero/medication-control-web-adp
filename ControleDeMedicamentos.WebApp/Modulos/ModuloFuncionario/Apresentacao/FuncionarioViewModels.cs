using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Apresentacao;

public record ListarFuncionarioViewModel(
    string Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record CadastrarFuncionarioViewModel(
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo 'Telefone' deve seguir o formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
    [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "O campo 'CPF' deve conter exatamente 11 dígitos.")]
    string Cpf
);

public record EditarFuncionarioViewModel(
    string Id,

    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo 'Telefone' deve seguir o formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
    [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "O campo 'CPF' deve conter exatamente 11 dígitos.")]
    string Cpf
);

public record ExcluirFuncionarioViewModel(
    string Id,
    string Nome,
    string Telefone,
    string Cpf
);