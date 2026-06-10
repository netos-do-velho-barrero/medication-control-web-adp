using System;
using System.ComponentModel.DataAnnotations;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;


namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;

public record ListarPacienteViewModel(
    string Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record CadastrarPacienteViewModel(
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo 'Telefone' deve seguir o formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo 'Cartão do SUS' é obrigatório.")]
    [RegularExpression(@"^(\s*\d){15}\s*$", ErrorMessage = "O campo 'Cartão do SUS' deve conter exatamente 15 dígitos.")]
    string CartaoSus,

    [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
    [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "O campo 'CPF' deve conter exatamente 11 dígitos.")]
    string Cpf
);

public record EditarPacienteViewModel(
    string Id,

    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo 'Telefone' é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo 'Telefone' deve seguir o formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
    string Telefone,

    [Required(ErrorMessage = "O campo 'Cartão do SUS' é obrigatório.")]
    [RegularExpression(@"^(\s*\d){15}\s*$", ErrorMessage = "O campo 'Cartão do SUS' deve conter exatamente 15 dígitos.")]
    string CartaoSus,

    [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
    [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "O campo 'CPF' deve conter exatamente 11 dígitos.")]
    string Cpf

);

public record ExcluirPacienteViewModel(
    string Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);