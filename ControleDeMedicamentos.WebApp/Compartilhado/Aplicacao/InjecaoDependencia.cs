using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
// using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
// using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
//using ControleDeMedicamentos.WebApp.ModuloEstoque.Aplicacao;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Clear();

            options.ViewLocationFormats.Add("/Modulos/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add("/Modulos/ModuloEstoque/{1}/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(config =>
        {
            config.AddProfile<FornecedorProfile>();
            config.AddProfile<MedicamentoProfile>();
            config.AddProfile<RequisicaoEntradaProfile>();
        });
    }
}
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoPaciente>();
        services.AddScoped<ServicoFuncionario>();
        services.AddScoped<ServicoMedicamento>();
        // services.AddScoped<ServicoRequisicaoEntrada>();
        // services.AddScoped<ServicoRequisicaoSaida>();

    }
}
