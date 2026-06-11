using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Aplicacao;

public class ServicoRequisicaoEntrada
{
    private readonly IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoRequisicaoEntrada(
        IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public ResultadoOperacaoRequisicaoEntrada Cadastrar(CadastrarRequisicaoEntradaDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);

        if (medicamento == null)
            return ResultadoOperacaoRequisicaoEntrada.Falha("Medicamento não encontrado.");

        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.FuncionarioId);

        if (funcionario == null)
            return ResultadoOperacaoRequisicaoEntrada.Falha("Funcionário não encontrado.");

        RequisicaoEntrada requisicaoEntrada = new RequisicaoEntrada(
            dto.Data,
            medicamento.Id,
            medicamento.Nome,
            funcionario.Id,
            funcionario.Nome,
            dto.Quantidade
        );

        List<string> erros = requisicaoEntrada.Validar();

        if (erros.Any())
            return ResultadoOperacaoRequisicaoEntrada.Falha(erros.First());

        medicamento.AdicionarQuantidade(dto.Quantidade);

        repositorioMedicamento.Editar(medicamento.Id, medicamento);
        repositorioRequisicaoEntrada.Cadastrar(requisicaoEntrada);

        return ResultadoOperacaoRequisicaoEntrada.Sucesso();
    }

    public ResultadoOperacaoRequisicaoEntrada Editar(EditarRequisicaoEntradaDto dto)
    {
        RequisicaoEntrada? requisicaoOriginal = repositorioRequisicaoEntrada.SelecionarPorId(dto.Id);

        if (requisicaoOriginal == null)
            return ResultadoOperacaoRequisicaoEntrada.Falha("Requisição de entrada não encontrada.");

        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);

        if (medicamento == null)
            return ResultadoOperacaoRequisicaoEntrada.Falha("Medicamento não encontrado.");

        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.FuncionarioId);

        if (funcionario == null)
            return ResultadoOperacaoRequisicaoEntrada.Falha("Funcionário não encontrado.");

        
        int estoqueTemporario = medicamento.QuantidadeEmEstoque - requisicaoOriginal.Quantidade;

        if (estoqueTemporario < 0)
            return ResultadoOperacaoRequisicaoEntrada.Falha("A alteração desta entrada não é permitida pois o estoque atual é menor que o estorno da quantidade anterior.");

   
        medicamento.SubtrairQuantidade(requisicaoOriginal.Quantidade);
        medicamento.AdicionarQuantidade(dto.Quantidade);

        RequisicaoEntrada requisicaoAtualizada = new RequisicaoEntrada(
            dto.Data,
            medicamento.Id,
            medicamento.Nome,
            funcionario.Id,
            funcionario.Nome,
            dto.Quantidade
        );

        List<string> erros = requisicaoAtualizada.Validar();

        if (erros.Any())
            return ResultadoOperacaoRequisicaoEntrada.Falha(erros.First());

        repositorioMedicamento.Editar(medicamento.Id, medicamento);
        repositorioRequisicaoEntrada.Editar(dto.Id, requisicaoAtualizada);

        return ResultadoOperacaoRequisicaoEntrada.Sucesso();
    }

    public ResultadoOperacaoRequisicaoEntrada Excluir(ExcluirRequisicaoEntradaDto dto)
    {
        RequisicaoEntrada? requisicaoEntrada = repositorioRequisicaoEntrada.SelecionarPorId(dto.Id);

        if (requisicaoEntrada == null)
            return ResultadoOperacaoRequisicaoEntrada.Falha("Requisição de entrada não encontrada.");

        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(requisicaoEntrada.MedicamentoId);

        if (medicamento != null)
        {
        
            if (medicamento.QuantidadeEmEstoque - requisicaoEntrada.Quantidade < 0)
                return ResultadoOperacaoRequisicaoEntrada.Falha("Não é possível excluir esta entrada. A quantidade atual em estoque é menor do que a quantidade que será estornada.");

       
            medicamento.SubtrairQuantidade(requisicaoEntrada.Quantidade);
            repositorioMedicamento.Editar(medicamento.Id, medicamento);
        }

        bool conseguiuExcluir = repositorioRequisicaoEntrada.Excluir(dto.Id);

        if (!conseguiuExcluir)
            return ResultadoOperacaoRequisicaoEntrada.Falha("Não foi possível excluir a requisição de entrada.");

        return ResultadoOperacaoRequisicaoEntrada.Sucesso();
    }

    public List<ListarRequisicaoEntradaDto> SelecionarTodos()
    {
        return repositorioRequisicaoEntrada
            .SelecionarTodos()
            .Select(r => new ListarRequisicaoEntradaDto(
                r.Id,
                r.Data,
                r.MedicamentoNome,
                r.FuncionarioNome,
                r.Quantidade
            ))
            .ToList();
    }

    public EditarRequisicaoEntradaDto? SelecionarPorId(string id)
    {
        RequisicaoEntrada? r = repositorioRequisicaoEntrada.SelecionarPorId(id);

        if (r == null) return null;

        return new EditarRequisicaoEntradaDto(r.Id, r.Data, r.MedicamentoId, r.FuncionarioId, r.Quantidade);
    }
}

public class ResultadoOperacaoRequisicaoEntrada
{
    public bool Conseguiu { get; }
    public string? MensagemErro { get; }

    private ResultadoOperacaoRequisicaoEntrada(bool conseguiu, string? mensagemErro = null)
    {
        Conseguiu = conseguiu;
        MensagemErro = mensagemErro;
    }

    public static ResultadoOperacaoRequisicaoEntrada Sucesso()
    {
        return new ResultadoOperacaoRequisicaoEntrada(true);
    }

    public static ResultadoOperacaoRequisicaoEntrada Falha(string mensagemErro)
    {
        return new ResultadoOperacaoRequisicaoEntrada(false, mensagemErro);
    }
}