using System;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;

public record ListarFuncionarioDto(
    string Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record CadastrarFuncionarioDto(
    string Nome,
    string Telefone,
    string Cpf
);

public record EditarFuncionarioDto(
    string Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record DetalhesFuncionarioDto(
    string Id,
    string Nome,
    string Telefone,
    string Cpf
);