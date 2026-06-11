using System;
using System.Collections.Generic;
using System.Linq;
using FluentResults;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;

public class ServicoFuncionario
{
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public Result Cadastrar(CadastrarFuncionarioDto dto)
    {
        if (ExisteFuncionarioComNome(dto.Nome))
            return Falha("Nome", "Já existe um funcionário com esse nome.");

        Funcionario novoFuncionario = new Funcionario(
            dto.Nome,
            dto.Telefone,
            dto.Cpf
        );

        repositorioFuncionario.Cadastrar(novoFuncionario);

        return Result.Ok();
    }

    public Result Editar(EditarFuncionarioDto dto)
    {
        if (ExisteFuncionarioComNome(dto.Nome, dto.Id))
            return Falha("Nome", "Já existe um funcionário com esse nome.");

        Funcionario funcionarioAtualizado = new Funcionario(
            dto.Nome,
            dto.Telefone,
            dto.Cpf
        );

        bool conseguiuEditar = repositorioFuncionario.Editar(dto.Id, funcionarioAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Funcionário não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        repositorioFuncionario.Excluir(id);

        return Result.Ok();
    }

    public List<ListarFuncionarioDto> SelecionarTodos()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        return funcionarios
            .Select(f => new ListarFuncionarioDto(f.Id,
                f.Nome,
                f.Telefone,
                f.Cpf))
            .ToList();
    }

    public Result<DetalhesFuncionarioDto> SelecionarPorId(string id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        return Result.Ok(new DetalhesFuncionarioDto(funcionario.Id,
            funcionario.Nome,
            funcionario.Telefone,
            funcionario.Cpf
        ));
    }

    private bool ExisteFuncionarioComNome(string nome, string? idIgnorado = null)
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        foreach (Funcionario f in funcionarios)
        {
            if (f.Id != idIgnorado && string.Equals(f.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}