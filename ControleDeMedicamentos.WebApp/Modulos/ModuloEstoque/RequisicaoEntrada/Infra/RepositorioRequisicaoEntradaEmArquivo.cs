using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Infra;

public class RepositorioRequisicaoEntradaEmArquivo
    : RepositorioBaseEmArquivo<RequisicaoEntrada>, IRepositorioRequisicaoEntrada
{
    public RepositorioRequisicaoEntradaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoEntrada> CarregarRegistros()
    {
        return contexto.RequisicoesEntrada;
    }
}