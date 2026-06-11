using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Dominio;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Infra;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Infra;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Infra;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Infra;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Infra;

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

        services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorEmArquivo>();
        services.AddScoped<IRepositorioMedicamento, RepositorioMedicamentoEmArquivo>();
        services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmArquivo>();
        services.AddScoped<IRepositorioRequisicaoEntrada, RepositorioRequisicaoEntradaEmArquivo>();
         services.AddScoped<IRepositorioPaciente, RepositorioPacienteEmArquivo>();
        // services.AddScoped<IRepositorioListaCompra, RepositorioListaCompraEmArquivo>();
        // services.AddScoped<IRepositorioItemLista, RepositorioItemListaEmArquivo>();
    }
}