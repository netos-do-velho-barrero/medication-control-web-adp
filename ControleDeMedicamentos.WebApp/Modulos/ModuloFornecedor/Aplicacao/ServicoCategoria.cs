using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoFornecedor(IRepositorioFornecedor repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public ResultadoOperacao Cadastrar(CadastrarFornecedorDto dto)
    {
        Fornecedor fornecedor = new Fornecedor(dto.Nome, dto.Telefone, dto.Cnpj);

        List<string> erros = fornecedor.Validar();

        if (erros.Any())
            return ResultadoOperacao.Falha(erros.First());

        if (repositorioFornecedor.SelecionarPorCnpj(fornecedor.Cnpj) != null)
            return ResultadoOperacao.Falha("Já existe um fornecedor cadastrado com este CNPJ.");

        repositorioFornecedor.Cadastrar(fornecedor);

        return ResultadoOperacao.Sucesso();
    }

    public ResultadoOperacao Editar(EditarFornecedorDto dto)
    {
        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(dto.Id)!;

        if (fornecedorSelecionado == null)
            return ResultadoOperacao.Falha("Fornecedor não encontrado.");

        Fornecedor fornecedorAtualizado = new Fornecedor(dto.Nome, dto.Telefone, dto.Cnpj);

        List<string> erros = fornecedorAtualizado.Validar();

        if (erros.Any())
            return ResultadoOperacao.Falha(erros.First());

        Fornecedor? fornecedorComMesmoCnpj = repositorioFornecedor.SelecionarPorCnpj(fornecedorAtualizado.Cnpj);

        if (fornecedorComMesmoCnpj != null && fornecedorComMesmoCnpj.Id != dto.Id)
            return ResultadoOperacao.Falha("Já existe um fornecedor cadastrado com este CNPJ.");

        repositorioFornecedor.Editar(dto.Id, fornecedorAtualizado);

        return ResultadoOperacao.Sucesso();
    }

    public ResultadoOperacao Excluir(string id)
    {
        bool conseguiuExcluir = repositorioFornecedor.Excluir(id);

        if (!conseguiuExcluir)
            return ResultadoOperacao.Falha("Fornecedor não encontrado.");

        return ResultadoOperacao.Sucesso();
    }

    public List<ListarFornecedorDto> SelecionarTodos()
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new ListarFornecedorDto(f.Id, f.Nome, f.Telefone, f.Cnpj))
            .ToList();
    }

    public EditarFornecedorDto? SelecionarPorId(string id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
            return null;

        return new EditarFornecedorDto(fornecedor.Id, fornecedor.Nome, fornecedor.Telefone, fornecedor.Cnpj);
    }

    public ExcluirFornecedorDto? SelecionarParaExclusao(string id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
            return null;

        return new ExcluirFornecedorDto(fornecedor.Id, fornecedor.Nome, fornecedor.Cnpj);
    }
}

public class ResultadoOperacao
{
    public bool Conseguiu { get; }

    public string? MensagemErro { get; }

    private ResultadoOperacao(bool conseguiu, string? mensagemErro = null)
    {
        Conseguiu = conseguiu;
        MensagemErro = mensagemErro;
    }

    public static ResultadoOperacao Sucesso()
    {
        return new ResultadoOperacao(true);
    }

    public static ResultadoOperacao Falha(string mensagemErro)
    {
        return new ResultadoOperacao(false, mensagemErro);
    }
}