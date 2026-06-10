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