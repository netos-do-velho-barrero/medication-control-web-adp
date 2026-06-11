using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Apresentacao;



namespace ControleDeMedicamentos.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoMedicamento>();
        services.AddScoped<ServicoPaciente>();
        services.AddScoped<ServicoFuncionario>();
        services.AddScoped<ServicoRequisicaoEntrada>();
        services.AddScoped<ServicoRequisicaoSaida>();


        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Clear();

            options.ViewLocationFormats.Add("/Modulos/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(config =>
        {
            config.AddProfile<FornecedorProfile>();
            config.AddProfile<MedicamentoProfile>();
            config.AddProfile<PacienteProfile>();
            config.AddProfile<FuncionarioProfile>();
            config.AddProfile<RequisicaoEntradaProfile>();
            config.AddProfile<RequisicaoSaidaProfile>();

        });

    }
}