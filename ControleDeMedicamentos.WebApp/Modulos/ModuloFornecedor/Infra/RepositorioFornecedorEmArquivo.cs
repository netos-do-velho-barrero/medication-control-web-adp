using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Infra;

public class RepositorioFornecedorEmArquivo : RepositorioBaseEmArquivo<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedorEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> CarregarRegistros()
    {
        return contexto.Fornecedores;
    }

    public Fornecedor? SelecionarPorCnpj(string cnpj)
    {
        string cnpjSomenteDigitos = Fornecedor.ObterSomenteDigitos(cnpj);

        return registros.FirstOrDefault(f => f.Cnpj == cnpjSomenteDigitos);
    }
}