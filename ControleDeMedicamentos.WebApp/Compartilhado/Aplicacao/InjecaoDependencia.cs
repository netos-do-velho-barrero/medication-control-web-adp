// using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
 using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
// using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
// using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
// using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // services.AddScoped<ServicoFornecedor>();
         services.AddScoped<ServicoPaciente>();
        // services.AddScoped<ServicoFuncionario>();
        // services.AddScoped<ServicoMedicamento>();
        // services.AddScoped<ServicoRequisicaoEntrada>();
        // services.AddScoped<ServicoRequisicaoSaida>();

    }
}
