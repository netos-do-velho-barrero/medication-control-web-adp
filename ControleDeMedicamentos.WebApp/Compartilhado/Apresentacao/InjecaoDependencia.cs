using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoMedicamento>();
        services.AddScoped<ServicoRequisicaoEntrada>();
    }
}