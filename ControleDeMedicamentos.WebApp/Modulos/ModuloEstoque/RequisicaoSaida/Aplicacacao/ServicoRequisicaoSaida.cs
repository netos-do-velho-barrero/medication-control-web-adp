using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Aplicacao;

public class ServicoRequisicaoSaida
{
    private readonly IRepositorioRequisicaoSaida repositorioRequisicaoSaida;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoRequisicaoSaida(
        IRepositorioRequisicaoSaida repositorioRequisicaoSaida,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioRequisicaoSaida = repositorioRequisicaoSaida;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
    }

    public ResultadoOperacaoRequisicaoSaida Cadastrar(CadastrarRequisicaoSaidaDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);

        if (medicamento == null)
            return ResultadoOperacaoRequisicaoSaida.Falha("Medicamento não encontrado.");

        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.PacienteId);

        if (paciente == null)
            return ResultadoOperacaoRequisicaoSaida.Falha("Paciente não encontrado.");

        if (dto.Quantidade > medicamento.QuantidadeEmEstoque)
            return ResultadoOperacaoRequisicaoSaida.Falha("A quantidade requisitada excede o estoque disponível.");

        RequisicaoSaida requisicaoSaida = new RequisicaoSaida(
            dto.Data,
            medicamento.Id,
            medicamento.Nome,
            paciente.Id,
            paciente.Nome,
            dto.Quantidade
        );

        List<string> erros = requisicaoSaida.Validar();

        if (erros.Any())
            return ResultadoOperacaoRequisicaoSaida.Falha(erros.First());

        medicamento.SubtrairQuantidade(dto.Quantidade);

        repositorioMedicamento.Editar(medicamento.Id, medicamento);
        repositorioRequisicaoSaida.Cadastrar(requisicaoSaida);

        return ResultadoOperacaoRequisicaoSaida.Sucesso();
    }

    public ResultadoOperacaoRequisicaoSaida Editar(EditarRequisicaoSaidaDto dto)
    {
        RequisicaoSaida? requisicaoOriginal = repositorioRequisicaoSaida.SelecionarPorId(dto.Id);

        if (requisicaoOriginal == null)
            return ResultadoOperacaoRequisicaoSaida.Falha("Requisição de saída não encontrada.");

        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);

        if (medicamento == null)
            return ResultadoOperacaoRequisicaoSaida.Falha("Medicamento não encontrado.");

        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.PacienteId);

        if (paciente == null)
            return ResultadoOperacaoRequisicaoSaida.Falha("Paciente não encontrado.");

       
        int estoqueTemporario = medicamento.QuantidadeEmEstoque + requisicaoOriginal.Quantidade;

        if (dto.Quantidade > estoqueTemporario)
            return ResultadoOperacaoRequisicaoSaida.Falha("A quantidade requisitada excede o estoque disponível.");

    
        medicamento.AdicionarQuantidade(requisicaoOriginal.Quantidade);
        medicamento.SubtrairQuantidade(dto.Quantidade);

        
        RequisicaoSaida requisicaoAtualizada = new RequisicaoSaida(
            dto.Data,
            medicamento.Id,
            medicamento.Nome,
            paciente.Id,
            paciente.Nome,
            dto.Quantidade
        );

        List<string> erros = requisicaoAtualizada.Validar();

        if (erros.Any())
            return ResultadoOperacaoRequisicaoSaida.Falha(erros.First());

      
        repositorioMedicamento.Editar(medicamento.Id, medicamento);
        repositorioRequisicaoSaida.Editar(dto.Id, requisicaoAtualizada);

        return ResultadoOperacaoRequisicaoSaida.Sucesso();
    }

    public ResultadoOperacaoRequisicaoSaida Excluir(ExcluirRequisicaoSaidaDto dto)
{
 
    RequisicaoSaida? requisicaoSaida = repositorioRequisicaoSaida.SelecionarPorId(dto.Id);

    if (requisicaoSaida == null)
        return ResultadoOperacaoRequisicaoSaida.Falha("Requisição de saída não encontrada.");

    Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(requisicaoSaida.MedicamentoId);

    if (medicamento != null)
    {
      
        medicamento.AdicionarQuantidade(requisicaoSaida.Quantidade);
        repositorioMedicamento.Editar(medicamento.Id, medicamento);
    }

    bool conseguiuExcluir = repositorioRequisicaoSaida.Excluir(dto.Id);

    if (!conseguiuExcluir)
        return ResultadoOperacaoRequisicaoSaida.Falha("Não foi possível excluir a requisição de saída.");

    return ResultadoOperacaoRequisicaoSaida.Sucesso();
}

    public List<ListarRequisicaoSaidaDto> SelecionarTodos()
    {
        return repositorioRequisicaoSaida
            .SelecionarTodos()
            .Select(r => new ListarRequisicaoSaidaDto(
                r.Id,
                r.Data,
                r.MedicamentoNome,
                r.PacienteNome,
                r.Quantidade
            ))
            .ToList();
    }

    public EditarRequisicaoSaidaDto? SelecionarPorId(string id)
    {
        RequisicaoSaida? r = repositorioRequisicaoSaida.SelecionarPorId(id);

        if (r == null) return null;

        return new EditarRequisicaoSaidaDto(r.Id, r.Data, r.MedicamentoId, r.PacienteId, r.Quantidade);
    }
}

public class ResultadoOperacaoRequisicaoSaida
{
    public bool Conseguiu { get; }
    public string? MensagemErro { get; }

    private ResultadoOperacaoRequisicaoSaida(bool conseguiu, string? mensagemErro = null)
    {
        Conseguiu = conseguiu;
        MensagemErro = mensagemErro;
    }

    public static ResultadoOperacaoRequisicaoSaida Sucesso()
    {
        return new ResultadoOperacaoRequisicaoSaida(true);
    }

    public static ResultadoOperacaoRequisicaoSaida Falha(string mensagemErro)
    {
        return new ResultadoOperacaoRequisicaoSaida(false, mensagemErro);
    }
}