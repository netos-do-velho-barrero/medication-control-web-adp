using System;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;


public record ListarPacienteDto(
    string Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);


public record CadastrarPacienteDto(
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record EditarPacienteDto(
  string Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);
public record DetalhesPacienteDto(
  string Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);