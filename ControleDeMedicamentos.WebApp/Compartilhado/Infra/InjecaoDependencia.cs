using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloCategoria.Dominio;
using ControleDeMedicamentos.WebApp.ModuloCategoria.Infra;
// using ControleDeMedicamentos.WebApp.ModuloItemLista.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloItemLista.Infra;
// using ControleDeMedicamentos.WebApp.ModuloListaCompra.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloListaCompra.Infra;
// using ControleDeMedicamentos.WebApp.ModuloProduto.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloProduto.Infra;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        // services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
         services.AddScoped<IRepositorioPaciente, RepositorioPacienteEmArquivo>();
        // services.AddScoped<IRepositorioListaCompra, RepositorioListaCompraEmArquivo>();
        // services.AddScoped<IRepositorioItemLista, RepositorioItemListaEmArquivo>();
    }
}