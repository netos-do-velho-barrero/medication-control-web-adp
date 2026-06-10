using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

public class ServicoMedicamento
{
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoMedicamento(
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFornecedor repositorioFornecedor)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public ResultadoOperacaoMedicamento Cadastrar(CadastrarMedicamentoDto dto)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedor == null)
            return ResultadoOperacaoMedicamento.Falha("Fornecedor não encontrado.");

        Medicamento medicamento = new Medicamento(
            dto.Nome,
            dto.Descricao,
            dto.QuantidadeEmEstoque,
            fornecedor.Id,
            fornecedor.Nome
        );

        List<string> erros = medicamento.Validar();

        if (erros.Any())
            return ResultadoOperacaoMedicamento.Falha(erros.First());

        Medicamento? medicamentoExistente = repositorioMedicamento.SelecionarPorNome(medicamento.Nome);

        if (medicamentoExistente != null)
        {
            medicamentoExistente.AdicionarQuantidade(medicamento.QuantidadeEmEstoque);
            medicamentoExistente.Descricao = medicamento.Descricao;
            medicamentoExistente.FornecedorId = medicamento.FornecedorId;
            medicamentoExistente.FornecedorNome = medicamento.FornecedorNome;

            repositorioMedicamento.Editar(medicamentoExistente.Id, medicamentoExistente);

            return ResultadoOperacaoMedicamento.Sucesso();
        }

        repositorioMedicamento.Cadastrar(medicamento);

        return ResultadoOperacaoMedicamento.Sucesso();
    }

    public ResultadoOperacaoMedicamento Editar(EditarMedicamentoDto dto)
    {
        Medicamento? medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(dto.Id);

        if (medicamentoSelecionado == null)
            return ResultadoOperacaoMedicamento.Falha("Medicamento não encontrado.");

        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedor == null)
            return ResultadoOperacaoMedicamento.Falha("Fornecedor não encontrado.");

        Medicamento medicamentoAtualizado = new Medicamento(
            dto.Nome,
            dto.Descricao,
            dto.QuantidadeEmEstoque,
            fornecedor.Id,
            fornecedor.Nome
        );

        List<string> erros = medicamentoAtualizado.Validar();

        if (erros.Any())
            return ResultadoOperacaoMedicamento.Falha(erros.First());

        repositorioMedicamento.Editar(dto.Id, medicamentoAtualizado);

        return ResultadoOperacaoMedicamento.Sucesso();
    }

    public ResultadoOperacaoMedicamento Excluir(string id)
    {
        bool conseguiuExcluir = repositorioMedicamento.Excluir(id);

        if (!conseguiuExcluir)
            return ResultadoOperacaoMedicamento.Falha("Medicamento não encontrado.");

        return ResultadoOperacaoMedicamento.Sucesso();
    }

    public List<ListarMedicamentoDto> SelecionarTodos()
    {
        return repositorioMedicamento
            .SelecionarTodos()
            .Select(m => new ListarMedicamentoDto(
                m.Id,
                m.Nome,
                m.Descricao,
                m.QuantidadeEmEstoque,
                m.FornecedorNome,
                m.EstaEmFalta()
            ))
            .ToList();
    }

    public EditarMedicamentoDto? SelecionarPorId(string id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return null;

        return new EditarMedicamentoDto(
            medicamento.Id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.QuantidadeEmEstoque,
            medicamento.FornecedorId
        );
    }

    public ExcluirMedicamentoDto? SelecionarParaExclusao(string id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return null;

        return new ExcluirMedicamentoDto(
            medicamento.Id,
            medicamento.Nome,
            medicamento.FornecedorNome
        );
    }
}

public class ResultadoOperacaoMedicamento
{
    public bool Conseguiu { get; }
    public string? MensagemErro { get; }

    private ResultadoOperacaoMedicamento(bool conseguiu, string? mensagemErro = null)
    {
        Conseguiu = conseguiu;
        MensagemErro = mensagemErro;
    }

    public static ResultadoOperacaoMedicamento Sucesso()
    {
        return new ResultadoOperacaoMedicamento(true);
    }

    public static ResultadoOperacaoMedicamento Falha(string mensagemErro)
    {
        return new ResultadoOperacaoMedicamento(false, mensagemErro);
    }
}