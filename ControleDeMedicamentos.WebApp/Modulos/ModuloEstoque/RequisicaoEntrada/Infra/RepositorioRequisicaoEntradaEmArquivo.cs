using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicaoEntrada.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicaoEntrada.Infra;

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