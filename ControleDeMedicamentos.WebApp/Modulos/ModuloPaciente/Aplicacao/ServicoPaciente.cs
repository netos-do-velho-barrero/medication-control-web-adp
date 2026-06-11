using System;
using FluentResults;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;


namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(
        IRepositorioPaciente repositorioPaciente 
    )
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public Result Cadastrar(CadastrarPacienteDto dto)
    {
        if (ExistePacienteComNome(dto.Nome))
            return Falha("Nome", "Já existe um paciente com esse nome.");

        Paciente novaPaciente = new Paciente(
            dto.Nome,
            dto.Telefone,
            dto.CartaoSus,
            dto.Cpf
        );

        repositorioPaciente.Cadastrar(novaPaciente);

        return Result.Ok();
    }

    public Result Editar(EditarPacienteDto dto)
    {
        if (ExistePacienteComNome(dto.Nome, dto.Id))
            return Falha("Nome", "Já existe um Paciente com esse nome.");

        Paciente categoriaAtualizada = new Paciente(dto.Nome,
            dto.Telefone,
            dto.CartaoSus,
            dto.Cpf);

        bool conseguiuEditar = repositorioPaciente.Editar(dto.Id, categoriaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Paciente não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(string id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("paciente não encontrada.");


        repositorioPaciente.Excluir(id);

        return Result.Ok();
    }

    public List<ListarPacienteDto> SelecionarTodos()
    {
        List<Paciente> paciente = repositorioPaciente.SelecionarTodos();

        return paciente
            .Select(p => new ListarPacienteDto(p.Id,
            p.Nome,
            p.Telefone,
            p.CartaoSus,
            p.Cpf))
            .ToList();
    }

    public Result<DetalhesPacienteDto> SelecionarPorId(string id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("paciente não encontrada.");

        return Result.Ok(new DetalhesPacienteDto(paciente.Id,
        paciente.Nome,
        paciente.Telefone,
        paciente.CartaoSus,
        paciente.Cpf
        ));
    }

    private bool ExistePacienteComNome(string nome, string? idIgnorado = null)
    {
        List<Paciente> paciente = repositorioPaciente.SelecionarTodos();

        foreach (Paciente p in paciente)
        {
            if (p.Id != idIgnorado && string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase))
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